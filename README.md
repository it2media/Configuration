# IT2media.Extensions.Configuration

```cs
namespace MyCompany.MyApplication.Options
{
	public class HomeControllerOptions
	{
		public bool IsFeatureEnabled { get; set; }
		public int TimeoutForSomething { get; set; }
		public string Message { get; set; }
	}
}
```

This is a sample for an `appsettings.json`.

It has a `Options`-Section with two Subsections `HomeControllerOptions` and `CompanyControllerOptions`.

```json
{
  "Options":
  {
    "HomeControllerOptions":
    {
      "IsFeatureEnabled": true,
	  "TimeoutForSomething": 5,
	  "Message": "My Message For You"
    },
	"CompanyControllerOptions":
    {
      "IsEastereggEnabled": false,
	  "CompanyName": "My Awesome Company",	  
    }
  }
}
```

Now in the Startup.cs you only need to use the `InitOptions` extension method of your `IConfiguration` and pass the `IServiceCollection` where the `IOptions<T>` of your *"Options"*

```cs
public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		//This initializes all models in *.Options namespace ending with "Options", for example the "HomeControllerOptions" in the "MyCompany.MyWebApplication.Options" namespace
		Configuration.InitOptions(services);
		
		//...
	}
}
```

Now your `IOptions<HomeControllerOptions>` are registered they are automatically injected with their bound values from your 'appsettings.json` to your Controller.

```cs
public class HomeController : Controller
{
    private readonly IOptions<HomeControllerOptions> _homeControllerOptions;

    public HomeController(IOptions<HomeControllerOptions> homeControllerOptions)
    {
        _homeControllerOptions = homeControllerOptions;
    }
    
    public IActionResult SelectElementList()
    {
        ViewData["Message"] = _homeControllerOptions.Value.Message;

        return View();
    }
}
```


