using D328.Domain.DomainService;
using D328.Domain.Model;
using System.Collections.Generic;
using Xunit;

namespace D328.Domain.Test.DomainService
{
    public class LineDomainServiceTest
    {
        [Fact(DisplayName = "正：Lines.Count()=Zero -> 1")]
        [Trait("LineDomainService", "CalcNewSortNumber")]
        public void CalcNewSortNumberTrue1()
        {
            var lines = new List<Line>();

            var actual = LineDomainService.CalcNewSortNumber(lines);

            Assert.Equal(1, actual);
        }

        [Fact(DisplayName = "正：Lines.Count() > 1 -> Lines.Max() + 1")]
        [Trait("LineDomainService", "CalcNewSortNumber")]
        public void CalcNewSortNumberTrue2()
        {
            var lines = new List<Line>()
            {
                Line.CreateNew(sortNumber: 1),
                Line.CreateNew(sortNumber: 3)
            };

            var actual = LineDomainService.CalcNewSortNumber(lines);

            Assert.Equal(4, actual);
        }

        [Fact(DisplayName = "正：null -> 1")]
        [Trait("LineDomainService", "CalcNewSortNumber")]
        public void CalcNewSortNumberFalse1()
        {
            var actual = LineDomainService.CalcNewSortNumber(null);

            Assert.Equal(1, actual);
        }
    }
}
