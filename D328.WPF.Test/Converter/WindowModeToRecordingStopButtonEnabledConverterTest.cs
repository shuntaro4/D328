using D328.WPF.Converter;
using System;
using Xunit;
using static D328.WPF.ViewModels.MainWindowViewModel;

namespace D328.WPF.Test.Converter
{
    public class WindowModeToRecordingStopButtonEnabledConverterTest
    {
        private readonly WindowModeToRecordingStopButtonEnabledConverter target = new WindowModeToRecordingStopButtonEnabledConverter();

        [Fact(DisplayName = "正：Enable=false(WindowMode=Normal)")]
        [Trait("WindowModeToRecordingStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue1()
        {
            var value = MainWindowMode.Normal;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=true(WindowMode=Recording)")]
        [Trait("WindowModeToRecordingStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue2()
        {
            var value = MainWindowMode.Recording;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=true(WindowMode=Pause)")]
        [Trait("WindowModeToRecordingStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue3()
        {
            var value = MainWindowMode.Pause;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(WindowMode=Playing)")]
        [Trait("WindowModeToRecordingStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue4()
        {
            var value = MainWindowMode.Playing;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：value=null")]
        [Trait("WindowModeToRecordingStopButtonEnabledConverter", "Convert")]
        public void ConvertFalse1()
        {
            var actual = target.Convert(null, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：未実装")]
        [Trait("WindowModeToRecordingStopButtonEnabledConverter", "ConvertBack")]
        public void ConvertBackFalse1()
        {
            Assert.ThrowsAny<NotImplementedException>(() => { target.ConvertBack(null, null, null, null); });
        }
    }
}
