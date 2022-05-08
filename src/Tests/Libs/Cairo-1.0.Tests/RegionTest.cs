using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class RegionTest : Test
    {
        [TestMethod]
        public void BindingsShouldSucceed()
        {
            // Default constructor.
            var empty_region = new Region();
            empty_region.Status.Should().Be(Status.Success);
            empty_region.IsEmpty.Should().Be(true);

            // Rectangle constructor and accessors.
            var rect_init = new RectangleInt() { X = 2, Y = 3, Height = 5, Width = 7 };
            var region = new Region(rect_init);
            region.Status.Should().Be(Status.Success);
            region.IsEmpty.Should().Be(false);
            region.NumRectangles.Should().Be(1);

            var rect_copy = new RectangleInt();
            region.GetRectangle(0, rect_copy);
            rect_copy.Should().Be(rect_init);

            rect_copy = new RectangleInt();
            region.GetExtents(rect_copy);
            rect_copy.Should().Be(rect_init);

            region.Translate(1, 1);
            rect_copy = new RectangleInt();
            region.GetExtents(rect_copy);
            rect_copy.Should().Be(new RectangleInt { X = 3, Y = 4, Height = 5, Width = 7 });

            // Copying
            var region_copy = region.Copy();
            region_copy.Should().NotBeSameAs(rect_copy);
            region_copy.NumRectangles.Should().Be(1);

            // Queries
            region.ContainsPoint(4, 4).Should().Be(true);
            region.ContainsPoint(1, 1).Should().Be(false);
            region.ContainsRectangle(rect_init).Should().Be(RegionOverlap.Part);

            // Boolean operations
            region.Intersect(region_copy).Should().Be(Status.Success);
            region.Union(region_copy).Should().Be(Status.Success);
            region.Subtract(region_copy).Should().Be(Status.Success);
            region.Xor(region_copy).Should().Be(Status.Success);
            region.IntersectRectangle(rect_init).Should().Be(Status.Success);
            region.UnionRectangle(rect_init).Should().Be(Status.Success);
            region.SubtractRectangle(rect_init).Should().Be(Status.Success);
            region.XorRectangle(rect_init).Should().Be(Status.Success);
        }
    }
}
