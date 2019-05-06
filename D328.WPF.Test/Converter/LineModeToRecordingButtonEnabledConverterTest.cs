using D328.Domain.Enum;
using D328.WPF.Converter;
using System;
using Xunit;

namespace D328.WPF.Test.Converter
{
    public class LineModeToRecordingButtonEnabledConverterTest
    {
        private readonly LineModeToRecordingButtonEnabledConverter target = new LineModeToRecordingButtonEnabledConverter();

        [Fact(DisplayName = "正：Enable=true(LineMode=Normal)")]
        [Trait("LineModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue1()
        {
            var value = LineMode.Normal;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(LineMode=Recording)")]
        [Trait("LineModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue2()
        {
            var value = LineMode.Recording;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(LineMode=Pause)")]
        [Trait("LineModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue3()
        {
            var value = LineMode.Pause;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(LineMode=Playing)")]
        [Trait("LineModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue4()
        {
            var value = LineMode.Playing;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：value=null")]
        [Trait("LineModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertFalse1()
        {
            var actual = target.Convert(null, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：未実装")]
        [Trait("LineModeToRecordingButtonEnabledConverter", "ConvertBack")]
        public void ConvertBackFalse1()
        {
            Assert.ThrowsAny<NotImplementedException>(() => { target.ConvertBack(null, null, null, null); });
        }
    }
}
