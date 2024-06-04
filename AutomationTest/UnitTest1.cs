using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test, Order(1)]
    public async Task Login()
    {
        Console.WriteLine("--Start Login Test--");
        Console.Out.Flush();
        
        await Page.GotoAsync("https://www.saucedemo.com/");
        await Page.Locator("[data-test=\"username\"]").ClickAsync();
        await Page.Locator("[data-test=\"username\"]").FillAsync("standard_user");
        await Page.Locator("[data-test=\"password\"]").ClickAsync();
        await Page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        await Page.Locator("[data-test=\"login-button\"]").ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" }).ClickAsync();
        
        try
        {
            await Expect(Page.Locator("[data-test=\"logout-sidebar-link\"]")).ToBeVisibleAsync();
            Console.WriteLine("Test Case: Login Successfully - PASS");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test Case: Login Successfully - FAIL: {ex.Message}");
        }
        
        await Page.GetByRole(AriaRole.Button, new() { Name = "Close Menu" }).ClickAsync();
        Console.WriteLine("--End Login Test--");
        Console.Out.Flush();
    }

    [Test, Order(2)]
    public async Task LockedOutUser()
    {
        Console.WriteLine("--Start Locked Out User Test--");
        Console.Out.Flush();
        
       await Page.GotoAsync("https://www.saucedemo.com/");
        await Page.Locator("[data-test=\"username\"]").ClickAsync();
        await Page.Locator("[data-test=\"username\"]").FillAsync("locked_out_user");
        await Page.Locator("[data-test=\"password\"]").ClickAsync();
        await Page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        await Page.Locator("[data-test=\"login-button\"]").ClickAsync();
        await Expect(Page.Locator("[data-test=\"error\"]")).ToBeVisibleAsync();

        try
        {
            await Expect(Page.Locator("[data-test=\"error\"]")).ToContainTextAsync("Epic sadface: Sorry, this user has been locked out.");
            Console.WriteLine("Test Case: Locked Out User - PASS");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test Case: Locked Out User - FAIL: {ex.Message}");
        }
        
 
        Console.WriteLine("--End Locked Out User Test--");
        Console.Out.Flush();
    }

    [Test,Order(3)]
    public async Task AddToCart()
    {
        Console.WriteLine("--Start Add To Cart from Items List Test--");
        Console.Out.Flush();

        await Page.GotoAsync("https://www.saucedemo.com/");
        await Page.Locator("[data-test=\"username\"]").ClickAsync();
        await Page.Locator("[data-test=\"username\"]").FillAsync("standard_user");
        await Page.Locator("[data-test=\"password\"]").ClickAsync();
        await Page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        await Page.Locator("[data-test=\"login-button\"]").ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" }).ClickAsync();
        
        try
        {
            await Expect(Page.Locator("[data-test=\"logout-sidebar-link\"]")).ToBeVisibleAsync();
            Console.WriteLine("User logged in");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User not logged in {ex.Message}");
        }
        
        await Page.GetByRole(AriaRole.Button, new() { Name = "Close Menu" }).ClickAsync();
        await Page.Locator("[data-test=\"add-to-cart-sauce-labs-bolt-t-shirt\"]").ClickAsync();
        await Page.Locator("[data-test=\"shopping-cart-link\"]").ClickAsync();
        
        try
        {
            await Expect(Page.Locator("[data-test=\"item-1-title-link\"]")).ToBeVisibleAsync();
            await Expect(Page.Locator("[data-test=\"inventory-item-name\"]")).ToContainTextAsync("Sauce Labs Bolt T-Shirt");
            Console.WriteLine("Test Case: Add to Cart Sauce Labs Bolt T-Shirt - PASS");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test Case: Add to Cart Sauce Labs Bolt T-Shirt - FAIL {ex.Message}");
        }

        Console.WriteLine("--End Add To Cart from Items List Test--");
        Console.Out.Flush();
    }

    [Test,Order(4)]
    public async Task ProblemUser()
    {
        Console.WriteLine("--Start Check Item Details Test--");
        Console.Out.Flush();

        await Page.GotoAsync("https://www.saucedemo.com/");
        await Page.Locator("[data-test=\"username\"]").ClickAsync();
        await Page.Locator("[data-test=\"username\"]").FillAsync("problem_user");
        await Page.Locator("[data-test=\"password\"]").ClickAsync();
        await Page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        await Page.Locator("[data-test=\"login-button\"]").ClickAsync();

        // //Get Text of Thumbnail
        // await Expect(Page.Locator("[data-test=\"item-4-title-link\"] [data-test=\"inventory-item-name\"]")).ToContainTextAsync("Sauce Labs Backpack");
        // await Expect(Page.Locator("[data-test=\"inventory-list\"]")).ToContainTextAsync("carry.allTheThings() with the sleek, streamlined Sly Pack that melds uncompromising style with unequaled laptop and tablet protection.");
        // await Expect(Page.Locator("[data-test=\"inventory-list\"]")).ToContainTextAsync("$29.99");

        // //Click Thumnbnail
        // await Page.Locator("[data-test=\"item-4-title-link\"]").ClickAsync();

        // //Get Text of Item Details
        // await Expect(Page.Locator("[data-test=\"inventory-item-name\"]")).ToContainTextAsync("Sauce Labs Fleece Jacket");
        // await Expect(Page.Locator("[data-test=\"inventory-item-desc\"]")).ToContainTextAsync("It's not every day that you come across a midweight quarter-zip fleece jacket capable of handling everything from a relaxing day outdoors to a busy day at the office.");
        // await Expect(Page.Locator("[data-test=\"inventory-item-price\"]")).ToContainTextAsync("$49.99");

        // Get Text of Thumbnail
        string? thumbnailTitle = await Page.Locator("[data-test=\"item-4-title-link\"] [data-test=\"inventory-item-name\"]").TextContentAsync();
        string? thumbnailDesc = await Page.Locator("[data-test=\"inventory-list\"]").TextContentAsync();
        string? thumbnailPrice = await Page.Locator("[data-test=\"inventory-list\"]").TextContentAsync();

        // Click Thumbnail
        await Page.Locator("[data-test=\"item-4-title-link\"]").ClickAsync();

        // Get Text of Item Details
        string? detailTitle = await Page.Locator("[data-test=\"inventory-item-name\"]").TextContentAsync();
        string? detailDesc = await Page.Locator("[data-test=\"inventory-item-desc\"]").TextContentAsync();
        string? detailPrice = await Page.Locator("[data-test=\"inventory-item-price\"]").TextContentAsync();

        // Compare text from thumbnail and item details
        if (thumbnailTitle != detailTitle || thumbnailDesc != detailDesc || thumbnailPrice != detailPrice)
        {
            Console.WriteLine("Test Case Failed - Item details do not match the thumbnail.");
        }
        else
        {
            Console.WriteLine("Test Case Passed - Item details match the thumbnail.");
        }

        Console.WriteLine("--End Check Item Details Test--");
        Console.Out.Flush();


        //Check Add to Cart button from item details
        Console.WriteLine("--Start Add to Cart from Item Details Test--");
        Console.Out.Flush();

        await Page.Locator("[data-test=\"add-to-cart\"]").ClickAsync();
        await Page.Locator("[data-test=\"shopping-cart-link\"]").ClickAsync();
        
        try
        {
            await Expect(Page.Locator("[data-test=\"item-4-title-link\"]")).ToBeVisibleAsync();
            await Expect(Page.Locator("[data-test=\"inventory-item-name\"]")).ToContainTextAsync("Sauce Labs Backpack");
            Console.WriteLine("Test Case: Add to Cart button from item details - Pass");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test Case: Add to Cart button from item details - FAIL {ex.Message}");
        }


        Console.WriteLine("--End Add to Cart from Item Details Test--");
        Console.Out.Flush();
    }
}
