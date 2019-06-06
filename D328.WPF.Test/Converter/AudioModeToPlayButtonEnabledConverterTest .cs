using D328.Domain.Enum;
using D328.WPF.Converter;
using System;
using Xunit;

namespace D328.WPF.Test.Converter
{
    public class AudioModeToPlayButtonEnabledConverterTest
    {
        private readonly AudioModeToPlayButtonEnabledConverter target = new AudioModeToPlayButtonEnabledConverter();

        [Fact(DisplayName = "正：Enable=true(AudioMode=Normal)")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertTrue1()
        {
            object[] values = { AudioMode.Normal, @"Resources\test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Recording)")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertTrue2()
        {
            object[] values = { AudioMode.Recording, @"Resources\test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Pause)")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertTrue3()
        {
            object[] values = { AudioMode.Pause, @"Resources\test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Playing)")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertTrue4()
        {
            object[] values = { AudioMode.Playing, @"Resources\test.txt" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：values=null")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertFalse1()
        {
            var actual = target.Convert(null, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：values.length != 2")]
        [Trait("AudioModeToPlayButtonEnabledConveter", "Convert")]
        public void ConvertFalse2()
        {
            object[] values = { "", "", "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=true(AudioMode=Normal、AudioPath=\"\")")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertFalse3()
        {
            object[] values = { AudioMode.Normal, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=true(AudioMode=Recording、AudioPath=\"\")")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertFalse4()
        {
            object[] values = { AudioMode.Recording, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=true(AudioMode=Pause、AudioPath=\"\")")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertFalse5()
        {
            object[] values = { AudioMode.Pause, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：Enable=true(AudioMode=Playing、AudioPath=\"\")")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertFalse6()
        {
            object[] values = { AudioMode.Playing, "" };

            var actual = target.Convert(values, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：未実装")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "ConvertBack")]
        public void ConvertBackFalse1()
        {
            Assert.ThrowsAny<NotImplementedException>(() => { target.ConvertBack(null, null, null, null); });
        }
    }
}
