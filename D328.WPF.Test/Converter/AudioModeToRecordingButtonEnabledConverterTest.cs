using D328.Domain.Enum;
using D328.WPF.Converter;
using System;
using Xunit;

namespace D328.WPF.Test.Converter
{
    public class AudioModeToRecordingButtonEnabledConverterTest
    {
        private readonly AudioModeToRecordingButtonEnabledConverter target = new AudioModeToRecordingButtonEnabledConverter();

        [Fact(DisplayName = "正：Enable=true(AudioMode=Normal)")]
        [Trait("AudioModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue1()
        {
            var value = AudioMode.Normal;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Recording)")]
        [Trait("AudioModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue2()
        {
            var value = AudioMode.Recording;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Pause)")]
        [Trait("AudioModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue3()
        {
            var value = AudioMode.Pause;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Playing)")]
        [Trait("AudioModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertTrue4()
        {
            var value = AudioMode.Playing;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：value=null")]
        [Trait("AudioModeToRecordingButtonEnabledConverter", "Convert")]
        public void ConvertFalse1()
        {
            var actual = target.Convert(null, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：未実装")]
        [Trait("AudioModeToRecordingButtonEnabledConverter", "ConvertBack")]
        public void ConvertBackFalse1()
        {
            Assert.ThrowsAny<NotImplementedException>(() => { target.ConvertBack(null, null, null, null); });
        }
    }
}
