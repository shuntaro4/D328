﻿using D328.Domain.Enum;
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
            var value = AudioMode.Normal;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Recording)")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertTrue2()
        {
            var value = AudioMode.Recording;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Pause)")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertTrue3()
        {
            var value = AudioMode.Pause;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.True(actual);
        }

        [Fact(DisplayName = "正：Enable=false(AudioMode=Playing)")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertTrue4()
        {
            var value = AudioMode.Playing;

            var actual = target.Convert(value, null, null, null) as bool?;

            Assert.False(actual);
        }

        [Fact(DisplayName = "異：value=null")]
        [Trait("AudioModeToPlayButtonEnabledConverter", "Convert")]
        public void ConvertFalse1()
        {
            var actual = target.Convert(null, null, null, null) as bool?;

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
