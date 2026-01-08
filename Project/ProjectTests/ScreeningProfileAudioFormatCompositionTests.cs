using Project.Classes;
using Project.Classes.AudioFormat;
using Project.Classes.PictureFormat;

namespace ProjectTests;

[TestFixture]
public class ScreeningProfileAudioFormatCompositionTests
{
    [SetUp]
    public void SetUp()
    {
        // Clear extents before each test
        ScreeningProfile.LoadExtent(null);
        Dolby.LoadExtent(null);
        Stereo.LoadExtent(null);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        ScreeningProfile.LoadExtent(null);
        Dolby.LoadExtent(null);
        Stereo.LoadExtent(null);
    }

    #region TwoD Tests

    [Test]
    public void TwoD_WithDolby_CreatesAndAddsToExtent()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Contains.Item(twoD));
            Assert.That(twoD.AudioFormat, Is.InstanceOf<Dolby>());
            Assert.That(((Dolby)twoD.AudioFormat).Type, Is.EqualTo(DolbyType.Atmos));
            Assert.That(((Dolby)twoD.AudioFormat).ScreeningProfile, Is.EqualTo(twoD));
            Assert.That(Dolby.Extent, Contains.Item((Dolby)twoD.AudioFormat));
        });
    }

    [Test]
    public void TwoD_WithStereo_CreatesAndAddsToExtent()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType.FullHD, 60, "Dolby Digital", "21:9");

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Contains.Item(twoD));
            Assert.That(twoD.AudioFormat, Is.InstanceOf<Stereo>());
            Assert.That(((Stereo)twoD.AudioFormat).AudioCodec, Is.EqualTo("Dolby Digital"));
            Assert.That(((Stereo)twoD.AudioFormat).ScreeningProfile, Is.EqualTo(twoD));
            Assert.That(Stereo.Extent, Contains.Item((Stereo)twoD.AudioFormat));
        });
    }

    [Test]
    public void TwoD_Remove_RemovesFromExtentAndDeletesAudioFormat()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        var dolby = (Dolby)twoD.AudioFormat;

        twoD.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Does.Not.Contain(twoD));
            Assert.That(Dolby.Extent, Does.Not.Contain(dolby));
        });
    }

    [Test]
    public void TwoD_Remove_WithStereo_RemovesFromExtentAndDeletesAudioFormat()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType.FullHD, 60, "Dolby Digital", "21:9");
        var stereo = (Stereo)twoD.AudioFormat;

        twoD.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Does.Not.Contain(twoD));
            Assert.That(Stereo.Extent, Does.Not.Contain(stereo));
        });
    }

    #endregion

    #region ThreeD Tests

    [Test]
    public void ThreeD_WithDolby_CreatesAndAddsToExtent()
    {
        var threeD = new ThreeD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, true);

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Contains.Item(threeD));
            Assert.That(threeD.AudioFormat, Is.InstanceOf<Dolby>());
            Assert.That(((Dolby)threeD.AudioFormat).Type, Is.EqualTo(DolbyType.Atmos));
            Assert.That(((Dolby)threeD.AudioFormat).ScreeningProfile, Is.EqualTo(threeD));
            Assert.That(threeD.GlassesRequired, Is.True);
            Assert.That(threeD.Polarized, Is.True);
        });
    }

    [Test]
    public void ThreeD_WithStereo_CreatesAndAddsToExtent()
    {
        var threeD = new ThreeD(ScreeningProfile.ResolutionType.FullHD, 60, "Legacy Codec", false);

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Contains.Item(threeD));
            Assert.That(threeD.AudioFormat, Is.InstanceOf<Stereo>());
            Assert.That(((Stereo)threeD.AudioFormat).AudioCodec, Is.EqualTo("Legacy Codec"));
            Assert.That(((Stereo)threeD.AudioFormat).ScreeningProfile, Is.EqualTo(threeD));
            Assert.That(threeD.GlassesRequired, Is.True);
            Assert.That(threeD.Polarized, Is.False);
        });
    }

    [Test]
    public void ThreeD_Remove_RemovesFromExtentAndDeletesAudioFormat()
    {
        var threeD = new ThreeD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, true);
        var dolby = (Dolby)threeD.AudioFormat;

        threeD.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Does.Not.Contain(threeD));
            Assert.That(Dolby.Extent, Does.Not.Contain(dolby));
        });
    }

    #endregion

    #region FormatImax Tests

    [Test]
    public void FormatImax_WithDolby_CreatesAndAddsToExtent()
    {
        var imax = new FormatImax(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, true);

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Contains.Item(imax));
            Assert.That(imax.AudioFormat, Is.InstanceOf<Dolby>());
            Assert.That(((Dolby)imax.AudioFormat).Type, Is.EqualTo(DolbyType.Atmos));
            Assert.That(((Dolby)imax.AudioFormat).ScreeningProfile, Is.EqualTo(imax));
            Assert.That(imax.GlassesRequired, Is.True);
            Assert.That(imax.Laser, Is.True);
        });
    }

    [Test]
    public void FormatImax_WithStereo_CreatesAndAddsToExtent()
    {
        var imax = new FormatImax(ScreeningProfile.ResolutionType.FullHD, 60, "IMAX Codec", false);

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Contains.Item(imax));
            Assert.That(imax.AudioFormat, Is.InstanceOf<Stereo>());
            Assert.That(((Stereo)imax.AudioFormat).AudioCodec, Is.EqualTo("IMAX Codec"));
            Assert.That(((Stereo)imax.AudioFormat).ScreeningProfile, Is.EqualTo(imax));
            Assert.That(imax.GlassesRequired, Is.True);
            Assert.That(imax.Laser, Is.False);
        });
    }

    [Test]
    public void FormatImax_Remove_RemovesFromExtentAndDeletesAudioFormat()
    {
        var imax = new FormatImax(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, true);
        var dolby = (Dolby)imax.AudioFormat;

        imax.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent, Does.Not.Contain(imax));
            Assert.That(Dolby.Extent, Does.Not.Contain(dolby));
        });
    }

    #endregion

    #region Composition Relationship Tests

    [Test]
    public void AudioFormat_CannotExistWithoutScreeningProfile()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        var dolby = (Dolby)twoD.AudioFormat;

        twoD.Remove();

        // After removing the picture format, accessing the audio format should fail
        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = dolby.ScreeningProfile;
        });
    }

    [Test]
    public void MultiplePictureFormats_CreateSeparateAudioFormats()
    {
        var twoD1 = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        var twoD2 = new TwoD(ScreeningProfile.ResolutionType.FullHD, 60, DolbyType.Digital, "21:9");
        var dolby1 = (Dolby)twoD1.AudioFormat;
        var dolby2 = (Dolby)twoD2.AudioFormat;

        Assert.Multiple(() =>
        {
            Assert.That(dolby1, Is.Not.EqualTo(dolby2));
            Assert.That(dolby1.ScreeningProfile, Is.EqualTo(twoD1));
            Assert.That(dolby2.ScreeningProfile, Is.EqualTo(twoD2));
            Assert.That(Dolby.Extent.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void RemovePictureFormat_DeletesOnlyItsAudioFormat()
    {
        var twoD1 = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        var twoD2 = new TwoD(ScreeningProfile.ResolutionType.FullHD, 60, DolbyType.Digital, "21:9");
        var dolby1 = (Dolby)twoD1.AudioFormat;
        var dolby2 = (Dolby)twoD2.AudioFormat;

        twoD1.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(Dolby.Extent, Does.Not.Contain(dolby1));
            Assert.That(Dolby.Extent, Contains.Item(dolby2));
            Assert.That(ScreeningProfile.Extent, Does.Not.Contain(twoD1));
            Assert.That(ScreeningProfile.Extent, Contains.Item(twoD2));
        });
    }

    #endregion

    #region Reverse Connection Tests

    [Test]
    public void Dolby_Remove_CallsScreeningProfileRemoveAudioFormat()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        var dolby = (Dolby)twoD.AudioFormat;

        dolby.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(Dolby.Extent, Does.Not.Contain(dolby));
            // The ScreeningProfile should have its audio format cleared
            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = twoD.AudioFormat;
            });
        });
    }

    [Test]
    public void Stereo_Remove_CallsScreeningProfileRemoveAudioFormat()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType.FullHD, 60, "Dolby Digital", "21:9");
        var stereo = (Stereo)twoD.AudioFormat;

        stereo.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(Stereo.Extent, Does.Not.Contain(stereo));
            // The ScreeningProfile should have its audio format cleared
            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = twoD.AudioFormat;
            });
        });
    }

    #endregion

    #region Extent Management Tests

    [Test]
    public void LoadExtent_ClearsAndReplacesExtent()
    {
        var twoD1 = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        var twoD2 = new TwoD(ScreeningProfile.ResolutionType.FullHD, 60, DolbyType.Digital, "21:9");

        Assert.That(ScreeningProfile.Extent.Count, Is.EqualTo(2));

        var newProfiles = new List<ScreeningProfile>
        {
            new TwoD(ScreeningProfile.ResolutionType._2K, 30, DolbyType.TrueHD, "16:9")
        };

        ScreeningProfile.LoadExtent(newProfiles);

        Assert.Multiple(() =>
        {
            Assert.That(ScreeningProfile.Extent.Count, Is.EqualTo(1));
            Assert.That(ScreeningProfile.Extent, Does.Not.Contain(twoD1));
            Assert.That(ScreeningProfile.Extent, Does.Not.Contain(twoD2));
        });
    }

    [Test]
    public void LoadExtent_Null_ClearsExtent()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");

        ScreeningProfile.LoadExtent(null);

        Assert.That(ScreeningProfile.Extent, Is.Empty);
    }

    [Test]
    public void LoadExtent_EmptyList_ClearsExtent()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");

        ScreeningProfile.LoadExtent(new List<ScreeningProfile>());

        Assert.That(ScreeningProfile.Extent, Is.Empty);
    }

    #endregion

    #region Property Tests

    [Test]
    public void TwoD_GlassesRequired_ReturnsFalse()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        Assert.That(twoD.GlassesRequired, Is.False);
    }

    [Test]
    public void ThreeD_GlassesRequired_ReturnsTrue()
    {
        var threeD = new ThreeD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, true);
        Assert.That(threeD.GlassesRequired, Is.True);
    }

    [Test]
    public void FormatImax_GlassesRequired_ReturnsTrue()
    {
        var imax = new FormatImax(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, true);
        Assert.That(imax.GlassesRequired, Is.True);
    }

    [Test]
    public void TwoD_AspectRatio_CanBeSet()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        twoD.AspectRatio = "21:9";
        Assert.That(twoD.AspectRatio, Is.EqualTo("21:9"));
    }

    [Test]
    public void TwoD_AspectRatio_EmptyString_ThrowsException()
    {
        var twoD = new TwoD(ScreeningProfile.ResolutionType._4K, 120, DolbyType.Atmos, "16:9");
        Assert.Throws<ArgumentException>(() => twoD.AspectRatio = "");
    }

    #endregion
}
