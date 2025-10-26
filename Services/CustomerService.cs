using DeliverySystem.Abstractions;
using DeliverySystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Services;

public class CustomerService : ICustomerService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public CustomerService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<RegistrationCustomerDto> CustomerLoginAsync([FromBody] LoginCustomerDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            throw new Exception("Invalid email or password");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
        {
            throw new Exception("Invalid email or password");
        }

        
        var claims = await _userManager.GetClaimsAsync(user);
        var name = claims.FirstOrDefault(c => c.Type == "Name")?.Value ?? "";
        var phone = claims.FirstOrDefault(c => c.Type == "Phone")?.Value ?? "";
        var address = claims.FirstOrDefault(c => c.Type == "Address")?.Value ?? "";

        return new RegistrationCustomerDto
        {
            Email = user.Email,
            Name = name,
            Phone = phone,
            Address = address
        };
    }

    public async Task<IdentityResult> AddCustomerAsync(RegistrationCustomerDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            PhoneNumber = dto.Phone
        };
        
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (result.Succeeded)
        {
            // Ensure Customer role exists
            var roleExists = await _userManager.IsInRoleAsync(user, "Customer");

            // Use RoleManager via service locator: get from _userManager's context - can't access RoleManager here, so rely on Role creation in Program.cs startup
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Name", dto.Name));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Phone", dto.Phone));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Address", dto.Address));

            // Assign role Customer
            await _userManager.AddToRoleAsync(user, "Customer");
        }
        
        return result;
    }

    public async Task<List<RegistrationCustomerDto>> GetAllCustomersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var result = new List<RegistrationCustomerDto>();
        
        foreach (var user in users)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var name = claims.FirstOrDefault(c => c.Type == "Name")?.Value ?? "";
            var phone = user.PhoneNumber ?? "";
            var address = claims.FirstOrDefault(c => c.Type == "Address")?.Value ?? "";
            
            result.Add(new RegistrationCustomerDto
            {
                Email = user.Email,
                Name = name,
                Phone = phone,
                Address = address
            });
        }
        
        return result;
    }

    public async Task<RegistrationCustomerDto> GetCustomerByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return null;
        }
        
        var claims = await _userManager.GetClaimsAsync(user);
        var name = claims.FirstOrDefault(c => c.Type == "Name")?.Value ?? "";
        var phone = user.PhoneNumber ?? "";
        var address = claims.FirstOrDefault(c => c.Type == "Address")?.Value ?? "";
        
        return new RegistrationCustomerDto
        {
            Email = user.Email,
            Name = name,
            Phone = phone,
            Address = address
        };
    }

    public async Task<IdentityResult> UpdateCustomerAsync(string email, RegistrationCustomerDto customer)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) throw new Exception("User not found");
        
        user.Email = customer.Email;
        user.UserName = customer.Email;
        user.PhoneNumber = customer.Phone;
        
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            
            var claims = await _userManager.GetClaimsAsync(user);
            
            
            var nameClaim = claims.FirstOrDefault(c => c.Type == "Name");
            if (nameClaim != null)
                await _userManager.RemoveClaimAsync(user, nameClaim);
            
            var addressClaim = claims.FirstOrDefault(c => c.Type == "Address");
            if (addressClaim != null)
                await _userManager.RemoveClaimAsync(user, addressClaim);
            
            
            
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Name", customer.Name));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Address", customer.Address));
            
        }
        
        return result;
    }

    public async Task<IdentityResult> DeleteCustomerAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return await _userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(string email, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        return result;
    }
}