using D328.WPF.Converter;
using System;
using Xunit;

namespace D328.WPF.Test.Converter
{
    public class NullToBoolValueConverterTest
    {
        private readonly NullToBoolValueConverter target = new NullToBoolValueConverter();

        [Fact(DisplayName = "正：null -> false")]
        [Trait("NullToBoolValueConverter", "Convert")]
        public void ConvertTrue1()
        {
            var actual = target.Convert(null, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：not null -> true")]
        [Trait("NullToBoolValueConverter", "Convert")]
        public void ConvertTrue2()
        {
            var value = 12345;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "異：未実装")]
        [Trait("NullToBoolValueConverter", "ConvertBack")]
        public void ConvertBackFalse1()
        {
            Assert.ThrowsAny<NotImplementedException>(() => { target.ConvertBack(null, null, null, null); });
        }
    }
}
