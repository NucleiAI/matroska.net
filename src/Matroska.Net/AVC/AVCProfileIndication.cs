namespace Matroska.Net.AVC
{
    public enum AVCProfileIndication : byte
    {
        BaseLineProfile = 66,
        ExtendedProfile = 88,
        MainProfile = 77,
        HighProfile = 100,
        High10Profile = 110,
        High422Profile = 122,
        High444PredictiveProfile = 244,
        CAVLC444IntraProfile = 44,
        ScalableBaselineProfile = 83,
        ScalableHighProfile = 86,
        StereoHighProfile = 128,
        MuliviewHighProfile = 118,
        MFCHighProfile = 134,
        MFCDepthHighProfile = 135,
        MultiviewDepthHighProfile = 138,
        EnhancedMultiviewDepthHighProfile = 139
    }
}
