# This project shows the usage of cookie for user authentication and authorization without Core Identity.

### Authorization Policies
The project defines one custom Authorization Policy along with Default Policy. Below are the requirement:
- AdminPolicy - Requires user role as Admin or SuperUser
```
builder.Services
    .AddAuthorization(configure =>
    {
        configure.AddPolicy("AdminPolicy", config =>
        {
            config.RequireClaim(ClaimTypes.Role, "SuperUser", "Admin");
        });
    });
```
- DefaultPolicy - No override done. By default, it look for authentication (if you're Authenticated, then you're Authorized!). Under the hood, it might look like as below:
```
DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
```


### Authorization Global Configuration
The Authorization is not enabled globally in this project. Only Admin action method in Home Controller is decorated with [Authorize("AdminPolicy")] (requires Authorization as per Admin Policy).

To enable authorization globally, use RequireAuthentication() while configuring endpoints.

```
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
```
In above case, the authorization will be done using Default Policy. To perform custom authorization, specific policy or list of policies can be provided to RequireAuthorization() to perform authorization accordingly.

```
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization("AdminPolicy");
```




