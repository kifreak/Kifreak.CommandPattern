using Kifreak.CommandPattern.Models;
using Kifreak.CommandPattern.Output;
using Xunit;

namespace Kifreak.CommandPattern.UnitTests
{
    public class ArgumentsUnitTests
    {
        [Fact]
        public void MainArgumentDetectorUnitTest()
        {
            BaseTestCommand test = new BaseTestCommand(new OutputConsole());
            test.MakeCommand(new Argument(new[] {"myCmd", "get", "all", "--no-devices", "-t", "testProbe"}));
            Assert.Equal("get", test.GetTypeCommand);
            Assert.Equal("all", test.GetSecondTypeCommand);
            Assert.True(test.NoDevices);
            Assert.False(test.NoExist);
            Assert.Equal("testProbe", test.ToText);
            Assert.Equal("This is an example", test.Example);

        }
    }
}
