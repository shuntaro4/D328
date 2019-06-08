using D328.Domain.Enum;
using D328.WPF.Converter;
using System;
using Xunit;

namespace D328.WPF.Test.Converter
{
    public class AudioModeToStopButtonEnabledConverterTest
    {
        private readonly AudioModeToStopButtonEnabledConverter target = new AudioModeToStopButtonEnabledConverter();

        [Fact(DisplayName = "正：Enable=true(AudioMode=Normal)")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue1()
        {
            object[] values = { AudioMode.Normal, @"Resources/test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Recording)")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue2()
        {
            object[] values = { AudioMode.Recording, @"Resources/test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Pause)")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue3()
        {
            object[] values = { AudioMode.Pause, @"Resources/test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Playing)")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertTrue4()
        {
            object[] values = { AudioMode.Playing, @"Resources/test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "異：value=null")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertFalse1()
        {
            var actual = target.Convert(null, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=true(AudioMode=Normal、AudioPath=\"\")")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertFalse2()
        {
            object[] values = { AudioMode.Normal, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=false(AudioMode=Recording、AudioPath=\"\")")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertFalse3()
        {
            object[] values = { AudioMode.Recording, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=false(AudioMode=Pause、AudioPath=\"\")")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertFalse4()
        {
            object[] values = { AudioMode.Pause, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=false(AudioMode=Playing、AudioPath=\"\")")]
        [Trait("AudioModeToStopButtonEnabledConverter", "Convert")]
        public void ConvertFalse5()
        {
            object[] values = { AudioMode.Playing, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }


        [Fact(DisplayName = "異：未実装")]
        [Trait("AudioModeToStopButtonEnabledConverter", "ConvertBack")]
        public void ConvertBackFalse1()
        {
            Assert.ThrowsAny<NotImplementedException>(() => { target.ConvertBack(null, null, null, null); });
        }
    }
}
