// Copyright Information
// ==================================
// SoftwareTesting - PlaywrightTests - UserInterfaceTests.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2022/07/22
// ==================================

namespace PlaywrightTests;

public class UserInterfaceTests
{
    //https://medium.com/version-1/playwright-a-modern-end-to-end-testing-for-web-app-with-c-language-support-c55e931273ee#:~
    [Fact]
    public static async Task VerifyGoogleSearchForPlaywright()
    {
        using IPlaywright playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = true, SlowMo = 50 });

        IBrowserContext context = await browser.NewContextAsync();

        IPage page = await context.NewPageAsync();
        //Navigate to letsusedata.com
        await page.GotoAsync("https://letsusedata.com/");
        // enter in username and password
        await page.FillAsync("xpath=//*[@id=\"txtUser\"]", "Test1");
        await page.FillAsync("xpath=//*[@id=\"txtPassword\"]", "12345678");
        // press login
        var response = await page.RunAndWaitForNavigationAsync(async () =>await page.ClickAsync("xpath=//*[@id=\"javascriptLogin\"]");
        // verify we have not changed page
        Assert.Equal("https://letsusedata.com/", page.Url);
        // verify it states invalid password on the screen
        page.GetByText("Invalid Password", new() { Exact = true });
        // time for round 2 which should succeed
        // go back to page just in case something went wrong above
        await page.GotoAsync("https://letsusedata.com/");
        // enter in username and password
        await page.FillAsync("xpath=//*[@id=\"txtUser\"]", "Test2");
        await page.FillAsync("xpath=//*[@id=\"txtPassword\"]", "iF3sBF7c");
        // Press login
        await page.ClickAsync("xpath=//*[@id=\"javascriptLogin\"]");
        //Verify Page URL
        Assert.Equal("https://letsusedata.com/CourseSelection.html", page.Url);
    }
}
