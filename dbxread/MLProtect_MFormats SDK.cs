//---------------------------------------------------------------------------
// MLProtect_MFormatsSDK.cs : Personal protection code for the Medialooks License system
//---------------------------------------------------------------------------
// Copyright (c) 2018, Medialooks Soft
// www.medialooks.com (dev@medialooks.com)
//
// Authors: Medialooks Team
// Version: 3.0.0.0
//
//---------------------------------------------------------------------------
// CONFIDENTIAL INFORMATION
//
// This file is Intellectual Property (IP) of Medialooks Soft and is
// strictly confidential. You can gain access to this file only if you
// sign a License Agreement and a Non-Disclosure Agreement (NDA) with
// Medialooks Soft. If you had not signed any of these documents, please
// contact <dev@medialooks.com> immediately.
//
//---------------------------------------------------------------------------
// Usage:
//
// 1. Add the reference to MLProxy.dll (located in the SDK /bin folder) in your C# project
//
// 2. Add this file to the project
//
// 3. Call MFormatsSDKLic.IntializeProtection() method before creating any Medialooks 
//    objects (for e.g. in Form_Load event handler)
//
// 4. Compile the project
//
// IMPORTANT: If you have several Medialooks products, don't forget to initialize
//            protection for all of them. For e.g.
//
//            MPlatformSDKLic.IntializeProtection();
//            DecoderlibLic.IntializeProtection();
//            etc.

using System;

    public class MFormatsSDKLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=PlayBox Neo Limited
License.CompanyID=13336
License.UUID={56A5AF3A-0F5D-4757-A1F8-A36EFB5C5909}
License.Key={97533FE5-EE1A-F832-C2BA-D70FEB7D4172}
License.Name=MFormats Module
License.UpdateExpirationDate=April 29, 2023
License.Edition=io_prof Professional gpu_h264
License.AllowedModule=*.*
License.Signature=138DE81554547DF1A77B692D6C30388E66EE5D4B6240613BFDDA45CA12AC4A2E9D4518EE83342FEE7586DBF68F707C324719708F4823E615B786DB9C7BA62DC744CAF0DCF9FD4E8165C80CC33DEFF1FB383DC47F800E4C17CABBDBC4B1CD7780B741B57886218E460D75768FA854FA043E0063134076A4FB4BE9859ED0B553C0

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=PlayBox Neo Limited
License.CompanyID=13336
License.UUID={5F8431D3-3925-46EB-A76E-57E2A66167CC}
License.Key={BE0393C9-7DB7-C69C-20AD-B868F61204E7}
License.Name=MFReader Module
License.UpdateExpirationDate=April 29, 2023
License.Edition=io_prof Professional gpu_h264
License.AllowedModule=*.*
License.Signature=91FCF3B8484F501727D9AA72D982BEE69ACF191F5FA95D298410857A08C22908952428AEE7C04F58B983BC7749CE67A4A3D88494133B1C1EB16B53347D3F1B238D0ADBB98858D28665C9AAC3658575CB3877E164228402F6AD643FCC8055DF220C7F3F5D61D906A6F8D5C895BB648998CFF8A8A66687524E5C829C39D4C79BA7

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=PlayBox Neo Limited
License.CompanyID=13336
License.UUID={728630A6-226B-4DA8-A478-B348463C8921}
License.Key={810E178D-D3BA-82BE-D9F8-F9F8DA289B33}
License.Name=MFRenderer Module
License.UpdateExpirationDate=April 29, 2023
License.Edition=io_prof Professional gpu_h264
License.AllowedModule=*.*
License.Signature=6E8C880A2777A3EC0BF8930504CDED567F7D5C038EADECB5254A72481CC1ED2E5489C23E020BE7DEFBF45EA24919AD87C60ADEEE69CCDEFB42D849831E92515278C60A1868FF853B05DF4692D7BB5E2224F778A77DE5BE54A6691071555D52D5700BF61F2230E75F88831B96EB66536A60BE93DD317F49A25915F8877115E7CC

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=PlayBox Neo Limited
License.CompanyID=13336
License.UUID={7D153526-AB47-45DD-843D-3E2048AE77F6}
License.Key={6F96641F-664F-8CF1-6CE2-42EEDA2A12C6}
License.Name=MFWriter Module
License.UpdateExpirationDate=April 29, 2023
License.Edition=io_prof Professional gpu_h264
License.AllowedModule=*.*
License.Signature=D80FC50C48EBC6FDCEE04D5B4E508444CC30C98199424122438E8F7D71E9CECBD1CD7FBBC5531C152D9E66AF4B43149581734CFA56D7B01F784306EDFDA18B08A1A9F87B348E85B3DB66DF6EA5EE5F8D24F6AE9EABD7DBF33EC93FED768578634C9C77E3D606284A9734220AC074EDE708E216989B3D1206F04BA9DA10D0B3C0

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=PlayBox Neo Limited
License.CompanyID=13336
License.UUID={EAE6335A-64C6-4FA9-8FA5-AE189863495E}
License.Key={B56E27FD-5124-AF44-932C-9E37F4795A60}
License.Name=Delay internal module
License.UpdateExpirationDate=April 29, 2023
License.Edition=io_prof Professional gpu_h264
License.AllowedModule=*.*
License.Signature=1BB952FEC95B14C468D221EC14C169AFF0FBB39077F5F729A8DF136CBA0E8FBB7AD684D7779B7DAF3EC2C2B79B95EDF761A6D5F1A5439EE54AA3FF79B12E5C9A0AF6FE76B3E47E92D1DDB39B40E308519E684A61A56EB11F48B5651604B5EF193B27BC54E4A7776E29DE9D62364E150F121C1C29EA1C3139989DA79BB3E1D636

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=PlayBox Neo Limited
License.CompanyID=13336
License.UUID={1DDF1905-DDA7-4329-90F5-60EF9EF20D7F}
License.Key={BE0393C9-8781-46A3-FC3C-13BEFD4F5E59}
License.Name=Medialooks DXGI Screen Capture
License.UpdateExpirationDate=April 29, 2023
License.Edition=Standard
License.AllowedModule=*.*
License.Signature=622C235D1D5A124264B4DF5E16CC3A8EEEAB88E8DDED732E623228D6A300BC986C265E4FFEA80F4DB8D3C91BB5C41B6C75CE1275486244F5C186974AE26DC39BD945C9F77FDBEC61518F73C88B6908D495EA320DA59425F4D7F287E0E2EC5101B7074F0D2803A71134BEAC8493CFD8FCD9602C4A37325AF84F16072F94624639

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=PlayBox Neo Limited
License.CompanyID=13336
License.UUID={A61F91D5-7103-4875-A215-5BF01DB4B9FD}
License.Key={8B3884A0-E743-27AB-621F-62A95F41C1AD}
License.Name=MWebRTC module
License.UpdateExpirationDate=April 29, 2023
License.Edition=Standard gpu_h264
License.AllowedModule=*.*
License.Signature=FCA149698E86EFBE49A20212FFBC723F3725A805DE86F8ED292B2F4B7A7274365AA17F90ED3400A5E0655C0911FB52C018DEA5987C883D1CE49EFCC135B3D9EDF043454C6BB5861B9356AB0E8C69CF860C8BD1E37AFF48FA8DFC03B8A370A6632904DDD973BB66D917710165663DC68060BF2F779A5C2A6FE4983F2F17DE575B

";

		//License initialization
        public static void IntializeProtection()
        {
            if (m_objMLProxy == null)
            {
                // Create MLProxy object 
                m_objMLProxy = new MLPROXYLib.CoMLProxyClass();
                m_objMLProxy.PutString(strLicInfo);                
            }
           UpdatePersonalProtection();
        }

        private static void UpdatePersonalProtection()
        {
            ////////////////////////////////////////////////////////////////////////
            // MediaLooks License secret key
            // Issued to: PlayBox Neo Limited
            const long _Q1_ = 65457047;
            const long _P1_ = 45761017;
            const long _Q2_ = 50317759;
            const long _P2_ = 48231433;

            try
            {

                int nFirst = 0;
                int nSecond = 0;
                m_objMLProxy.GetData(out nFirst, out  nSecond);

                // Calculate First * Q1 mod P1
                long llFirst = (long)nFirst * _Q1_ % _P1_;
                // Calculate Second * Q2 mod P2
                long llSecond = (long)nSecond * _Q2_ % _P2_;

                uint uRes = SummBits((uint)(llFirst + llSecond));

                // Calculate check value
                long llCheck = (long)(nFirst - 29) * (nFirst - 23) % nSecond;
                // Calculate return value
                int nRand = new Random().Next(0x7FFF);
                int nValue = (int)llCheck + (int)nRand * (uRes > 0 ? 1 : -1);

                m_objMLProxy.SetData(nFirst, nSecond, (int)llCheck, nValue);

            }
            catch (System.Exception) { }

        }

        private static uint SummBits(uint _nValue)
        {
            uint nRes = 0;
            while (_nValue > 0)
            {
                nRes += (_nValue & 1);
                _nValue >>= 1;
            }

            return nRes % 2;
        }
    }