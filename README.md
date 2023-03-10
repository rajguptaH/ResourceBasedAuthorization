# Resource Based Authorization in asp dot net core Mvc
- You Have To Tell In The Startup You Are going to use authentication write below code in start.cs and this says you are adding the authentication service in your application using a cookies
```c#
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
```
### Now You have To Add Singleton For Your Handler in Startup
```c#
builder.Services.AddSingleton<IAuthorizationHandler, ImageFileDTOHandler>();
```
### after that you have to tell the application to add authentication and authorization
```c#
app.UseAuthentication();
app.UseAuthorization();
```
### And Then Create a Handler For Source 
```c#
public class ImageFileDTOHandler : AuthorizationHandler<OperationAuthorizationRequirement, ImageFileDTO>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   OperationAuthorizationRequirement requirement,
                                                   ImageFileDTO resource)
    {
    //This is checking That this incoming requirment is meeting with read access or not
        if (requirement.Name == Operations.Read.Name )
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
public static class Operations 
{
    public static OperationAuthorizationRequirement Create =
        new OperationAuthorizationRequirement { Name = nameof(Create) };
    public static OperationAuthorizationRequirement Read =
        new OperationAuthorizationRequirement { Name = nameof(Read) };
    public static OperationAuthorizationRequirement Update =
        new OperationAuthorizationRequirement { Name = nameof(Update) };
    public static OperationAuthorizationRequirement Delete =
        new OperationAuthorizationRequirement { Name = nameof(Delete) };
}
```
### now you have to call This AuthorizeAsync Method With resource /Type and i have given a requirment Operations.Read 
```c#
ImageFileDTO imageFile = new ImageFileDTO();
            imageFile.Name = "admin";
            //It Will Automaticlly Call the ImageFileHandler because of ImageFileDTO Type we have passed 
 if ((await _authorizationService.AuthorizeAsync(User, imageFile, Operations.Read)).Succeeded)
            {
                var name = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
                var salary = HttpContext.User.FindFirst(c => c.Type == "Salary").Value;
                ViewBag.Name = name;
                ViewBag.Salary = salary;
                return View();
            }
```
- You Can apply any custom rules or Db Related rules which defines your Authorization For Particular user in Handler
## Thanks
- Visit To The Repository Where Role Based Authorization Has been implemented 
- [ResourceBaseAuthorization](https://github.com/rajguptaH/ResourceBasedAuthorization)
