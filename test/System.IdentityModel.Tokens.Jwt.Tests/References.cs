//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace System.IdentityModel.Tokens.Jwt.Tests
{
    // https://tools.ietf.org/html/rfc7518#appendix-A.3
    // B.1.  Test Cases for AES_128_CBC_HMAC_SHA_256

    public static class AES_128_CBC_HMAC_SHA_256
    {
        public static byte[] K
        {
            get
            {
                return new byte[]{ 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,
                                   0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f };
            }
        }

        public static byte[] MAC_KEY
        {
            get
            {
                return new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };
            }
        }

        public static byte[] ENC_KEY
        {
            get
            {
                return new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f };
            }
        }

        public static byte[] P
        {
            get
            {
                return new byte[] { 0x41, 0x20, 0x63, 0x69, 0x70, 0x68, 0x65, 0x72, 0x20, 0x73, 0x79, 0x73, 0x74, 0x65, 0x6d, 0x20,
                                    0x6d, 0x75, 0x73, 0x74, 0x20, 0x6e, 0x6f, 0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x65, 0x71, 0x75,
                                    0x69, 0x72, 0x65, 0x64, 0x20, 0x74, 0x6f, 0x20, 0x62, 0x65, 0x20, 0x73, 0x65, 0x63, 0x72, 0x65,
                                    0x74, 0x2c, 0x20, 0x61, 0x6e, 0x64, 0x20, 0x69, 0x74, 0x20, 0x6d, 0x75, 0x73, 0x74, 0x20, 0x62,
                                    0x65, 0x20, 0x61, 0x62, 0x6c, 0x65, 0x20, 0x74, 0x6f, 0x20, 0x66, 0x61, 0x6c, 0x6c, 0x20, 0x69,
                                    0x6e, 0x74, 0x6f, 0x20, 0x74, 0x68, 0x65, 0x20, 0x68, 0x61, 0x6e, 0x64, 0x73, 0x20, 0x6f, 0x66,
                                    0x20, 0x74, 0x68, 0x65, 0x20, 0x65, 0x6e, 0x65, 0x6d, 0x79, 0x20, 0x77, 0x69, 0x74, 0x68, 0x6f,
                                    0x75, 0x74, 0x20, 0x69, 0x6e, 0x63, 0x6f, 0x6e, 0x76, 0x65, 0x6e, 0x69, 0x65, 0x6e, 0x63, 0x65
                };
            }
        }

        public static byte[] IV
        {
            get
            {
                return new byte[] { 0x1a, 0xf3, 0x8c, 0x2d, 0xc2, 0xb9, 0x6f, 0xfd, 0xd8, 0x66, 0x94, 0x09, 0x23, 0x41, 0xbc, 0x04 };
            }
        }

        public static byte[] A
        {
            get
            {
                return new byte[] { 0x54, 0x68, 0x65, 0x20, 0x73, 0x65, 0x63, 0x6f, 0x6e, 0x64, 0x20, 0x70, 0x72, 0x69, 0x6e, 0x63,
                                    0x69, 0x70, 0x6c, 0x65, 0x20, 0x6f, 0x66, 0x20, 0x41, 0x75, 0x67, 0x75, 0x73, 0x74, 0x65, 0x20,
                                    0x4b, 0x65, 0x72, 0x63, 0x6b, 0x68, 0x6f, 0x66, 0x66, 0x73 };
            }
        }

        public static byte[] AL
        {
            get
            {
                return new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x50 };
            }
        }

        public static byte[] E
        {
            get
            {
                return new byte[] { 0xc8, 0x0e, 0xdf, 0xa3, 0x2d, 0xdf, 0x39, 0xd5, 0xef, 0x00, 0xc0, 0xb4, 0x68, 0x83, 0x42, 0x79,
                                    0xa2, 0xe4, 0x6a, 0x1b, 0x80, 0x49, 0xf7, 0x92, 0xf7, 0x6b, 0xfe, 0x54, 0xb9, 0x03, 0xa9, 0xc9,
                                    0xa9, 0x4a, 0xc9, 0xb4, 0x7a, 0xd2, 0x65, 0x5c, 0x5f, 0x10, 0xf9, 0xae, 0xf7, 0x14, 0x27, 0xe2,
                                    0xfc, 0x6f, 0x9b, 0x3f, 0x39, 0x9a, 0x22, 0x14, 0x89, 0xf1, 0x63, 0x62, 0xc7, 0x03, 0x23, 0x36,
                                    0x09, 0xd4, 0x5a, 0xc6, 0x98, 0x64, 0xe3, 0x32, 0x1c, 0xf8, 0x29, 0x35, 0xac, 0x40, 0x96, 0xc8,
                                    0x6e, 0x13, 0x33, 0x14, 0xc5, 0x40, 0x19, 0xe8, 0xca, 0x79, 0x80, 0xdf, 0xa4, 0xb9, 0xcf, 0x1b,
                                    0x38, 0x4c, 0x48, 0x6f, 0x3a, 0x54, 0xc5, 0x10, 0x78, 0x15, 0x8e, 0xe5, 0xd7, 0x9d, 0xe5, 0x9f,
                                    0xbd, 0x34, 0xd8, 0x48, 0xb3, 0xd6, 0x95, 0x50, 0xa6, 0x76, 0x46, 0x34, 0x44, 0x27, 0xad, 0xe5,
                                    0x4b, 0x88, 0x51, 0xff, 0xb5, 0x98, 0xf7, 0xf8, 0x00, 0x74, 0xb9, 0x47, 0x3c, 0x82, 0xe2, 0xdb };
            }
        }

        public static byte[] M
        {
            get
            {
                return new byte[] { 0x65, 0x2c, 0x3f, 0xa3, 0x6b, 0x0a, 0x7c, 0x5b, 0x32, 0x19, 0xfa, 0xb3, 0xa3, 0x0b, 0xc1, 0xc4,
                                    0xe6, 0xe5, 0x45, 0x82, 0x47, 0x65, 0x15, 0xf0, 0xad, 0x9f, 0x75, 0xa2, 0xb7, 0x1c, 0x73, 0xef };
            }
        }

        public static byte[] T
        {
            get
            {
                return new byte[] { 0x65, 0x2c, 0x3f, 0xa3, 0x6b, 0x0a, 0x7c, 0x5b, 0x32, 0x19, 0xfa, 0xb3, 0xa3, 0x0b, 0xc1, 0xc4 };
            }
        }
    }

    public static class RFC7520References
    {
        #region Keys
        // references from https://tools.ietf.org/html/rfc7520
        public static JsonWebKey ECDsaPrivateKey
        {
            get
            {
                return new JsonWebKey
                {
                    Crv = "P-521",
                    D = "AAhRON2r9cqXX1hg-RoI6R1tX5p2rUAYdmpHZoC1XNM56KtscrX6zbKipQrCW9CGZH3T4ubpnoTKLDYJ_fF3_rJt",
                    KeyId = "bilbo.baggins@hobbiton.example",
                    Kty = "EC",
                    X = "AHKZLLOsCOzz5cY97ewNUajB957y-C-U88c3v13nmGZx6sYl_oJXu9A5RkTKqjqvjyekWF-7ytDyRXYgCF5cj0Kt",
                    Y = "AdymlHvOiLxXkEhayXQnNCvDX4h9htZaCJN34kfmC6pV5OhQHiraVySsUdaQkAgDPrwQrJmbnX9cwlGfP-HqHZR1"
                };
            }
        }

        public static JsonWebKey ECDsaPublicKey
        {
            get
            {
                return new JsonWebKey
                {
                    Crv = "P-521",
                    KeyId = "bilbo.baggins@hobbiton.example",
                    Kty = "EC",
                    Use = "sig",
                    X = "AHKZLLOsCOzz5cY97ewNUajB957y-C-U88c3v13nmGZx6sYl_oJXu9A5RkTKqjqvjyekWF-7ytDyRXYgCF5cj0Kt",
                    Y = "AdymlHvOiLxXkEhayXQnNCvDX4h9htZaCJN34kfmC6pV5OhQHiraVySsUdaQkAgDPrwQrJmbnX9cwlGfP-HqHZR1"
                };
            }
        }

        public static string ES512Encoded
        {
            get { return "eyJhbGciOiJFUzUxMiIsImtpZCI6ImJpbGJvLmJhZ2dpbnNAaG9iYml0b24uZXhhbXBsZSJ9"; }
        }

        public static string ES512Header
        {
            get { return "{\"alg\":\"ES512\",\"kid\":\"bilbo.baggins@hobbiton.example\"}"; }
        }

        public static string ES512SignatureEncoded
        {
            get { return "AE_R_YZCChjn4791jSQCrdPZCNYqHXCTZH0-JZGYNlaAjP2kqaluUIIUnC9qvbu9Plon7KRTzoNEuT4Va2cmL1eJAQy3mtPBu_u_sDDyYjnAMDxXPn7XrT0lw-kvAD890jl8e2puQens_IEKBpHABlsbEPX6sFY8OcGDqoRuBomu9xQ2"; }
        }

        public static JwtHeader ES512JwtHeader
        {
            get
            {
                var header = new JwtHeader();
                header.Clear();
                header["alg"] = "ES512";
                header["kid"] = "bilbo.baggins@hobbiton.example";
                return header;
            }
        }

        #endregion Keys

        public static string Payload
        {
            get { return "It\u2019s a dangerous business, Frodo, going out your door. You step onto the road, and if you don't keep your feet, there\u2019s no knowing where you might be swept off to."; }
        }

        public static string PayloadEncoded
        {
            get { return "SXTigJlzIGEgZGFuZ2Vyb3VzIGJ1c2luZXNzLCBGcm9kbywgZ29pbmcgb3V0IHlvdXIgZG9vci4gWW91IHN0ZXAgb250byB0aGUgcm9hZCwgYW5kIGlmIHlvdSBkb24ndCBrZWVwIHlvdXIgZmVldCwgdGhlcmXigJlzIG5vIGtub3dpbmcgd2hlcmUgeW91IG1pZ2h0IGJlIHN3ZXB0IG9mZiB0by4"; }
        }

        // 5.1.  Key Encryption Using RSA v1.5 and AES-HMAC-SHA2
        // "alg" parameter of "RSA1_5".
        // "enc" parameter of "A128CBC-HS256".

        public static string RSA_1_5_IV
        {
            get { return "bbd5sTkYwhAIqfHsx8DayA"; }
        }

        public static string RSA_1_5_CEK
        {
            get { return "3qyTVhIWt5juqZUCpfRqpvauwB956MEJL2Rt-8qXKSo"; }
        }

        public static string RSA_1_5_CEKEncrypted
        {
            get { return "laLxI0j-nLH-_BgLOXMozKxmy9gffy2gTdvqzfTihJBuuzxg0V7yk1WClnQePFvG2K-pvSlWc9BRIazDrn50RcRai__3TDON395H3c62tIouJJ4XaRvYHFjZTZ2GXfz8YAImcc91Tfk0WXC2F5Xbb71ClQ1DDH151tlpH77f2ff7xiSxh9oSewYrcGTSLUeeCt36r1Kt3OSj7EyBQXoZlN7IxbyhMAfgIe7Mv1rOTOI5I8NQqeXXW8VlzNmoxaGMny3YnGir5Wf6Qt2nBq4qDaPdnaAuuGUGEecelIO1wx1BpyIfgvfjOhMBs9M8XL223Fg47xlGsMXdfuY-4jaqVw"; }
        }

        public static string RSA_1_5_ProtectedHeader
        {
            get
            {
                return "{\"alg\":\"RSA1_5\",\"kid\":\"frodo.baggins@hobbiton.example\",\"enc\":\"A128CBC-HS256\"}";
            }
        }

        public static string RSA_1_5_ProtectedHeaderEncoded
        {
            get
            {
                return "eyJhbGciOiJSU0ExXzUiLCJraWQiOiJmcm9kby5iYWdnaW5zQGhvYmJpdG9uLmV4YW1wbGUiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2In0";
            }
        }

        public static string RSA_1_5_AuthenticationTag
        {
            get
            {
                return "kvKuFBXHe5mQr4lqgobAUg";
            }
        }

        public static string RSA_1_5_JWE
        {
            get
            {
                return "eyJhbGciOiJSU0ExXzUiLCJraWQiOiJmcm9kby5iYWdnaW5zQGhvYmJpdG9uLmV4YW1wbGUiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2In0."
                     + "laLxI0j-nLH-_BgLOXMozKxmy9gffy2gTdvqzfTihJBuuzxg0V7yk1WClnQePFvG2K-pvSlWc9BRIazDrn50RcRai__3TDON395H3c62tIouJJ4XaRvYHFjZTZ2GXfz8YAImcc91Tfk0WXC2F5Xbb71ClQ1DDH151tlpH77f2ff7xiSxh9oSewYrcGTSLUeeCt36r1Kt3OSj7EyBQXoZlN7IxbyhMAfgIe7Mv1rOTOI5I8NQqeXXW8VlzNmoxaGMny3YnGir5Wf6Qt2nBq4qDaPdnaAuuGUGEecelIO1wx1BpyIfgvfjOhMBs9M8XL223Fg47xlGsMXdfuY-4jaqVw."
                     + "bbd5sTkYwhAIqfHsx8DayA."
                     + "0fys_TY_na7f8dwSfXLiYdHaA2DxUjD67ieF7fcVbIR62JhJvGZ4_FNVSiGc_raa0HnLQ6s1P2sv3Xzl1p1l_o5wR_RsSzrS8Z-wnI3Jvo0mkpEEnlDmZvDu_k8OWzJv7eZVEqiWKdyVzFhPpiyQU28GLOpRc2VbVbK4dQKPdNTjPPEmRqcaGeTWZVyeSUvf5k59yJZxRuSvWFf6KrNtmRdZ8R4mDOjHSrM_s8uwIFcqt4r5GX8TKaI0zT5CbL5Qlw3sRc7u_hg0yKVOiRytEAEs3vZkcfLkP6nbXdC_PkMdNS-ohP78T2O6_7uInMGhFeX4ctHG7VelHGiT93JfWDEQi5_V9UN1rhXNrYu-0fVMkZAKX3VWi7lzA6BP430m."
                     + "kvKuFBXHe5mQr4lqgobAUg";
            }
        }

        public static string RSA_1_5_CipherText
        {
            get
            {
                return "0fys_TY_na7f8dwSfXLiYdHaA2DxUjD67ieF7fcVbIR62JhJvGZ4_FNVSiGc_raa0HnLQ6s1P2sv3Xzl1p1l_o5wR_RsSzrS8Z-wnI3Jvo0mkpEEnlDmZvDu_k8OWzJv7eZVEqiWKdyVzFhPpiyQU28GLOpRc2VbVbK4dQKPdNTjPPEmRqcaGeTWZVyeSUvf5k59yJZxRuSvWFf6KrNtmRdZ8R4mDOjHSrM_s8uwIFcqt4r5GX8TKaI0zT5CbL5Qlw3sRc7u_hg0yKVOiRytEAEs3vZkcfLkP6nbXdC_PkMdNS-ohP78T2O6_7uInMGhFeX4ctHG7VelHGiT93JfWDEQi5_V9UN1rhXNrYu-0fVMkZAKX3VWi7lzA6BP430m";
            }
        }

        public static JsonWebKey RSA_1_5_PrivateKey
        {
            get
            {
                return new JsonWebKey
                {
                    D = "Kn9tgoHfiTVi8uPu5b9TnwyHwG5dK6RE0uFdlpCGnJN7ZEi963R7wybQ1PLAHmpIbNTztfrheoAniRV1NCIqXaW_qS461xiDTp4ntEPnqcKsyO5jMAji7-CL8vhpYYowNFvIesgMoVaPRYMYT9TW63hNM0aWs7USZ_hLg6Oe1mY0vHTI3FucjSM86Nff4oIENt43r2fspgEPGRrdE6fpLc9Oaq-qeP1GFULimrRdndm-P8q8kvN3KHlNAtEgrQAgTTgz80S-3VD0FgWfgnb1PNmiuPUxO8OpI9KDIfu_acc6fg14nsNaJqXe6RESvhGPH2afjHqSy_Fd2vpzj85bQQ",
                    DP = "UfYKcL_or492vVc0PzwLSplbg4L3-Z5wL48mwiswbpzOyIgd2xHTHQmjJpFAIZ8q-zf9RmgJXkDrFs9rkdxPtAsL1WYdeCT5c125Fkdg317JVRDo1inX7x2Kdh8ERCreW8_4zXItuTl_KiXZNU5lvMQjWbIw2eTx1lpsflo0rYU",
                    DQ = "iEgcO-QfpepdH8FWd7mUFyrXdnOkXJBCogChY6YKuIHGc_p8Le9MbpFKESzEaLlN1Ehf3B6oGBl5Iz_ayUlZj2IoQZ82znoUrpa9fVYNot87ACfzIG7q9Mv7RiPAderZi03tkVXAdaBau_9vs5rS-7HMtxkVrxSUvJY14TkXlHE",
                    E = "AQAB",
                    KeyId = "frodo.baggins@hobbiton.example",
                    Kty = "RSA",
                    N = "maxhbsmBtdQ3CNrKvprUE6n9lYcregDMLYNeTAWcLj8NnPU9XIYegTHVHQjxKDSHP2l-F5jS7sppG1wgdAqZyhnWvXhYNvcM7RfgKxqNx_xAHx6f3yy7s-M9PSNCwPC2lh6UAkR4I00EhV9lrypM9Pi4lBUop9t5fS9W5UNwaAllhrd-osQGPjIeI1deHTwx-ZTHu3C60Pu_LJIl6hKn9wbwaUmA4cR5Bd2pgbaY7ASgsjCUbtYJaNIHSoHXprUdJZKUMAzV0WOKPfA6OPI4oypBadjvMZ4ZAj3BnXaSYsEZhaueTXvZB4eZOAjIyh2e_VOIKVMsnDrJYAVotGlvMQ",
                    P = "2DwQmZ43FoTnQ8IkUj3BmKRf5Eh2mizZA5xEJ2MinUE3sdTYKSLtaEoekX9vbBZuWxHdVhM6UnKCJ_2iNk8Z0ayLYHL0_G21aXf9-unynEpUsH7HHTklLpYAzOOx1ZgVljoxAdWNn3hiEFrjZLZGS7lOH-a3QQlDDQoJOJ2VFmU",
                    Q = "te8LY4-W7IyaqH1ExujjMqkTAlTeRbv0VLQnfLY2xINnrWdwiQ93_VF099aP1ESeLja2nw-6iKIe-qT7mtCPozKfVtUYfz5HrJ_XY2kfexJINb9lhZHMv5p1skZpeIS-GPHCC6gRlKo1q-idn_qxyusfWv7WAxlSVfQfk8d6Et0",
                    QI = "kC-lzZOqoFaZCr5l0tOVtREKoVqaAYhQiqIRGL-MzS4sCmRkxm5vZlXYx6RtE1n_AagjqajlkjieGlxTTThHD8Iga6foGBMaAr5uR1hGQpSc7Gl7CF1DZkBJMTQN6EshYzZfxW08mIO8M6Rzuh0beL6fG9mkDcIyPrBXx2bQ_mM",
                    Use = "enc"
                };
            }
        }

        public static string RSAEncoded
        {
            get { return RSAHeaderEncoded + "." + PayloadEncoded; }
        }

        public static string RSAHeader
        {
            get { return "{\"alg\":\"RS256\",\"kid\":\"bilbo.baggins@hobbiton.example\"}"; }
        }

        public static string RSAHeaderEncoded
        {
            get { return "eyJhbGciOiJSUzI1NiIsImtpZCI6ImJpbGJvLmJhZ2dpbnNAaG9iYml0b24uZXhhbXBsZSJ9"; }
        }

        public static JwtHeader RSAJwtHeader
        {
            get
            {
                var header = new JwtHeader();
                header.Clear();
                header["alg"] = "RS256";
                header["kid"] = "bilbo.baggins@hobbiton.example";
                return header;
            }
        }

        public static JsonWebKey RSASigningPrivateKey
        {
            get
            {
                return new JsonWebKey
                {
                    D = "bWUC9B-EFRIo8kpGfh0ZuyGPvMNKvYWNtB_ikiH9k20eT-O1q_I78eiZkpXxXQ0UTEs2LsNRS-8uJbvQ-A1irkwMSMkK1J3XTGgdrhCku9gRldY7sNA_AKZGh-Q661_42rINLRCe8W-nZ34ui_qOfkLnK9QWDDqpaIsA-bMwWWSDFu2MUBYwkHTMEzLYGqOe04noqeq1hExBTHBOBdkMXiuFhUq1BU6l-DqEiWxqg82sXt2h-LMnT3046AOYJoRioz75tSUQfGCshWTBnP5uDjd18kKhyv07lhfSJdrPdM5Plyl21hsFf4L_mHCuoFau7gdsPfHPxxjVOcOpBrQzwQ",
                    DP = "B8PVvXkvJrj2L-GYQ7v3y9r6Kw5g9SahXBwsWUzp19TVlgI-YV85q1NIb1rxQtD-IsXXR3-TanevuRPRt5OBOdiMGQp8pbt26gljYfKU_E9xn-RULHz0-ed9E9gXLKD4VGngpz-PfQ_q29pk5xWHoJp009Qf1HvChixRX59ehik",
                    DQ = "CLDmDGduhylc9o7r84rEUVn7pzQ6PF83Y-iBZx5NT-TpnOZKF1pErAMVeKzFEl41DlHHqqBLSM0W1sOFbwTxYWZDm6sI6og5iTbwQGIC3gnJKbi_7k_vJgGHwHxgPaX2PnvP-zyEkDERuf-ry4c_Z11Cq9AqC2yeL6kdKT1cYF8",
                    E = "AQAB",
                    KeyId = "bilbo.baggins@hobbiton.example",
                    Kty = "RSA",
                    N = "n4EPtAOCc9AlkeQHPzHStgAbgs7bTZLwUBZdR8_KuKPEHLd4rHVTeT-O-XV2jRojdNhxJWTDvNd7nqQ0VEiZQHz_AJmSCpMaJMRBSFKrKb2wqVwGU_NsYOYL-QtiWN2lbzcEe6XC0dApr5ydQLrHqkHHig3RBordaZ6Aj-oBHqFEHYpPe7Tpe-OfVfHd1E6cS6M1FZcD1NNLYD5lFHpPI9bTwJlsde3uhGqC0ZCuEHg8lhzwOHrtIQbS0FVbb9k3-tVTU4fg_3L_vniUFAKwuCLqKnS2BYwdq_mzSnbLY7h_qixoR7jig3__kRhuaxwUkRz5iaiQkqgc5gHdrNP5zw",
                    P = "3Slxg_DwTXJcb6095RoXygQCAZ5RnAvZlno1yhHtnUex_fp7AZ_9nRaO7HX_-SFfGQeutao2TDjDAWU4Vupk8rw9JR0AzZ0N2fvuIAmr_WCsmGpeNqQnev1T7IyEsnh8UMt-n5CafhkikzhEsrmndH6LxOrvRJlsPp6Zv8bUq0k",
                    Q = "uKE2dh-cTf6ERF4k4e_jy78GfPYUIaUyoSSJuBzp3Cubk3OCqs6grT8bR_cu0Dm1MZwWmtdqDyI95HrUeq3MP15vMMON8lHTeZu2lmKvwqW7anV5UzhM1iZ7z4yMkuUwFWoBvyY898EXvRD-hdqRxHlSqAZ192zB3pVFJ0s7pFc",
                    QI = "3PiqvXQN0zwMeE-sBvZgi289XP9XCQF3VWqPzMKnIgQp7_Tugo6-NZBKCQsMf3HaEGBjTVJs_jcK8-TRXvaKe-7ZMaQj8VfBdYkssbu0NKDDhjJ-GtiseaDVWt7dcH0cfwxgFUHpQh7FoCrjFJ6h6ZEpMF6xmujs4qMpPz8aaI4",
                    Use = "sig"
                };
            }
        }

        public static JsonWebKey RSASigningPublicKey
        {
            get
            {
                return new JsonWebKey
                {
                    KeyId = "bilbo.baggins@hobbiton.example",
                    Kty = "RSA",
                    E = "AQAB",
                    N = "n4EPtAOCc9AlkeQHPzHStgAbgs7bTZLwUBZdR8_KuKPEHLd4rHVTeT-O-XV2jRojdNhxJWTDvNd7nqQ0VEiZQHz_AJmSCpMaJMRBSFKrKb2wqVwGU_NsYOYL-QtiWN2lbzcEe6XC0dApr5ydQLrHqkHHig3RBordaZ6Aj-oBHqFEHYpPe7Tpe-OfVfHd1E6cS6M1FZcD1NNLYD5lFHpPI9bTwJlsde3uhGqC0ZCuEHg8lhzwOHrtIQbS0FVbb9k3-tVTU4fg_3L_vniUFAKwuCLqKnS2BYwdq_mzSnbLY7h_qixoR7jig3__kRhuaxwUkRz5iaiQkqgc5gHdrNP5zw",
                    Use = "sig"
                };
            }
        }

        public static string RSASignatureEncoded
        {
            get { return "MRjdkly7_-oTPTS3AXP41iQIGKa80A0ZmTuV5MEaHoxnW2e5CZ5NlKtainoFmKZopdHM1O2U4mwzJdQx996ivp83xuglII7PNDi84wnB-BDkoBwA78185hX-Es4JIwmDLJK3lfWRa-XtL0RnltuYv746iYTh_qHRD68BNt1uSNCrUCTJDt5aAE6x8wW1Kt9eRo4QPocSadnHXFxnt8Is9UzpERV0ePPQdLuW3IS_de3xyIrDaLGdjluPxUAhb6L2aXic1U12podGU0KLUQSE_oI-ZnmKJ3F4uOZDnd6QZWJushZ41Axf_fcIe8u9ipH84ogoree7vjbU5y18kDquDg"; }
        }

        public static string SymmetricEncoded
        {
            get { return SymmetricHeaderEncoded + "." + PayloadEncoded; }
        }

        public static string SymmetricHeader
        {
            get { return "{\"alg\":\"HS256\",\"kid\":\"018c0ae5-4d9b-471b-bfd6-eef314bc7037\"}"; }
        }

        public static string SymmetricHeaderEncoded
        {
            get { return "eyJhbGciOiJIUzI1NiIsImtpZCI6IjAxOGMwYWU1LTRkOWItNDcxYi1iZmQ2LWVlZjMxNGJjNzAzNyJ9"; }
        }

        public static JwtHeader SymmetricJwtHeader
        {
            get
            {
                var header = new JwtHeader();
                header.Clear();
                header["alg"] = "HS256";
                header["kid"] = "018c0ae5-4d9b-471b-bfd6-eef314bc7037";
                return header;
            }
        }

        public static JsonWebKey SymmetricKeyEnc
        {
            get
            {
                return new JsonWebKey
                {
                    Alg = "A256GCM",
                    K = "AAPapAv4LbFbiVawEjagUBluYqN5rhna-8nuldDvOx8",
                    KeyId = "1e571774-2e08-40da-8308-e8d68773842d",
                    Kty = "oct",
                    Use = "sig"
                };
            }
        }
        public static JsonWebKey SymmetricKeyMac
        {
            get
            {
                return new JsonWebKey
                {
                    Alg = "HS256",
                    K = "hJtXIZ2uSN5kbQfbtTNWbpdmhkV8FJG-Onbc6mxCcYg",
                    KeyId = "018c0ae5-4d9b-471b-bfd6-eef314bc7037",
                    Kty = "oct",
                    Use = "sig"
                };
            }
        }

        public static string SymmetricSignatureEncoded
        {
            get { return "s0h6KThzkfBBBkLspW1h84VsJZFTsPPqMDA7g1Md7p0"; }
        }
    }

    public static class EncodedJwts
    {
        public static string Asymmetric_LocalSts =  @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpc3MiOiJodHRwOi8vR290Snd0LmNvbSIsImF1ZCI6Imh0dHA6Ly9Db250b3NvLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2NvdW50cnkiOiJVU0EiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c2VyQGNvbnRvc28uY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiVG9ueSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2hvbWVwaG9uZSI6IjU1NS4xMjEyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU2FsZXMiLCJzdWIiOiJib2IifQ.QW0Wfw-R5n3BHXE0vG-0giRFeB6W9oFrWJyFTaLI0qICDYx3yZ2eLXJ3zNFLVf3OG-MqytN5tqUdNfK1mRzeubqvdODHLFX36e1o3X8DR_YumyyQvgSeTJ0wwqT8PowbE3nbKfiX4TtJ4jffBelGKnL6vdx3AU2cwvLfSVp8ppA";
        public static string Asymmetric_1024 =      @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpc3MiOiJodHRwOi8vR290Snd0LmNvbSIsImF1ZCI6Imh0dHA6Ly9Db250b3NvLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2NvdW50cnkiOiJVU0EiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c2VyQGNvbnRvc28uY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiVG9ueSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2hvbWVwaG9uZSI6IjU1NS4xMjEyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU2FsZXMiLCJzdWIiOiJib2IifQ.WlNiBiAqmS4G-Em5O-uYiWLK5CJO8B-6Hvqjv_DXpoxldGiMWzivuyJocXPIIDVbcLxovmTc5j0KKgA9foOFBSkEEasqESA0VTYE30T1kkrGOaElola5DZagzax2zDipjxhbtBdMsvgF2t6GQJKyF0oFt828_yRGUsUnaXxg_MY";
        public static string Asymmetric_2048 =      @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpc3MiOiJodHRwOi8vR290Snd0LmNvbSIsImF1ZCI6Imh0dHA6Ly9Db250b3NvLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2NvdW50cnkiOiJVU0EiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c2VyQGNvbnRvc28uY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiVG9ueSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2hvbWVwaG9uZSI6IjU1NS4xMjEyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU2FsZXMiLCJzdWIiOiJib2IifQ.XYeDHk0XRs1ybrk2AMWu3ZwNC6gPUYqxacJtUDSfQCGouRFdmkYtZcgvWAhH8iFv3DmPgfX0lI9WCtjN2JOZqOx5w90r9UKCh_9e_vUKZyjLkyUEv3iBl2HTpxfcj3ns5MmZI50N8O2cYq1d6-CRK_oi8oKhLWKfrD8LoMpCtV8zjraEB1GUfJvMrxPTIzHSF-V_nmu5aPIoHVyxAcc1jShkYdnS5Dz8nVqLBleCAQ2Tv-8N9Q8l1362b088y15auc-hBb76KmMU2aCutyJDRz0NqsCkFz-cV-vnIj-hzl562DzSUP48nEMTwEIO_bRKex1R5beZ36ZrKLP1GQxc8Q";
        public static string Symmetric_256   =      @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOi8vR290Snd0LmNvbSIsImF1ZCI6Imh0dHA6Ly9Db250b3NvLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2NvdW50cnkiOiJVU0EiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c2VyQGNvbnRvc28uY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiVG9ueSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2hvbWVwaG9uZSI6IjU1NS4xMjEyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU2FsZXMiLCJzdWIiOiJib2IifQ._IFPA82MzKeV4IrsgZX8mkAEfzWT8-zEE4b5R2nzih4";
        public static string InvalidPayload =       @"eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsIng1dCI6InZ4VThJR1pYdEFtemg0NzdDT05CR2dYRTlfYyJ9.eyJcdWQiOiJodHRwOi8vbG9jYWxob3N0L1JQIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdC9TdHMiLCJuYmYiOjEzNjcyODA0MDUsImV4cCI6MTM2NzMwOTIwNSwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiYWFsIn0.Pro66IUD94jvZNnG_l96Hph78L_LYSx6eobO6QfWF3y038ebLZorhKYgAj1LtsNVAbq7E_I5tnoI1Y4YUV5_wMGtMqT_XTB4N8vktDzf0Y32MhopsDrveofJAAFAUP1npYZtFF89RAWzy1GaXqXw05SbUcyMPWTSvmPk_frzJRTc-utAaBAp-zKqS1KXGB_s99x7lDxy3ZFMDFtFHQlOJiXeClXYCVkB-ZmvrSFSAIasFK4eIG9pOcMY43_wS7ybNjF7WncY6PEi6JmUoh2AwA-SCdY-Bhs80Tf4GMB2HsmuMkSVgoptt6Fgf-q8LhWG0W80g66JRgdhMj85BZ6bxg";
        public static string LiveJwt        =       @"eyJhbGciOiJIUzI1NiIsImtpZCI6IjAiLCJ0eXAiOiJKV1QifQ.eyJ2ZXIiOjEsImlzcyI6InVybjp3aW5kb3dzOmxpdmVpZCIsImV4cCI6MTM2ODY0ODg2MywidWlkIjoiMzgwZTE3YzMxNGU2ZmMyODA0NzA3MjI5NTc3MjEwZmIiLCJhdWQiOiJ3d3cuc3JpLWRldjEwMC5jb20iLCJ1cm46bWljcm9zb2Z0OmFwcHVyaSI6Im1zLWFwcDovL1MtMS0xNS0yLTM2MzczOTQzNzAtMjIzMTgyMTkzNi01NjUwMTU1MS0xNTE0NjEzNDgyLTQ1NjgzNjc4LTM1NzUyNjE4NTItMjMzNTgyNjkwIiwidXJuOm1pY3Jvc29mdDphcHBpZCI6IjAwMDAwMDAwNEMwRTdBNUMifQ.I-sE7t6IJUho1TfgaLilNuzro-pWOMgg33rQ351GcoM";
        public static string OverClaims =           @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtyaU1QZG1Cdng2OHNrVDgtbVBBQjNCc2VlQSJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLndpbmRvd3MubmV0IiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3LyIsImlhdCI6MTQwNTk2ODkyMiwibmJmIjoxNDA1OTY4OTIyLCJleHAiOjE0MDU5NzI4MjIsInZlciI6IjEuMCIsInRpZCI6IjcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0NyIsImFtciI6WyJwd2QiXSwib2lkIjoiMzVjNzZlZWQtZjY0MC00YWU3LWFhZTItMzI3NzE3MWVhM2U1IiwidXBuIjoibmJhbGlnYUBtaWNyb3NvZnQuY29tIiwidW5pcXVlX25hbWUiOiJuYmFsaWdhQG1pY3Jvc29mdC5jb20iLCJzdWIiOiI1R0UwVkhBSlBuaUdNSWluN3dMNFBFMFE5MjAzTG00bHJBUnBrcEFBYmprIiwicHVpZCI6IjEwMDM3RkZFODAxQjI4QTAiLCJmYW1pbHlfbmFtZSI6IkJhbGlnYSIsImdpdmVuX25hbWUiOiJOYW1yYXRhIiwiX2NsYWltX25hbWVzIjp7Imdyb3VwcyI6InNyYzEifSwiX2NsYWltX3NvdXJjZXMiOnsic3JjMSI6eyJlbmRwb2ludCI6Imh0dHBzOi8vZ3JhcGgud2luZG93cy5uZXQvNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3L3VzZXJzLzM1Yzc2ZWVkLWY2NDAtNGFlNy1hYWUyLTMyNzcxNzFlYTNlNS9nZXRNZW1iZXJPYmplY3RzIn19LCJhcHBpZCI6IjExOGUxNzBmLWNmMjYtNDAwZi1hMGU5LTk2OTEwYjMxMTg3ZSIsImFwcGlkYWNyIjoiMSIsInNjcCI6IlVzZXJQcm9maWxlLlJlYWQiLCJhY3IiOiIxIn0.PWNfaBajC6KAr2dKiG0aJ1295hIXm9XWZPdrCw6zMgT0s46rrcBFMWOJQ-4Cz1aSqour6tslg8cl4_1rAjlkVwsXs7QTekMHxIcf3SPpM6vPTa7OfQ4dzBbPQV_QKif1xBXDkFQfZPAF2tPwcK_VBzHT0Z94_CpOtxChXmGEctW38Rt6f8bC_aaD6nsTZOt6NdAmI2AVOchpp7qNWEdBTvdcoNyz_a5VbUwWsHGCvozcOLjjFLles-K0BhiFw3MyJU_DMG-H6TgeBtwJPiuU2vHUTea26sfKHbpe7GypBo1PjY7odDWMH-d7c1Z0fT-UL15dAV419zX1NGbl-cujsw";
        public static string Cyrano = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtyaU1QZG1Cdng2OHNrVDgtbVBBQjNCc2VlQSJ9.eyJhdWQiOiJmZTc4ZTBiNC02ZmU3LTQ3ZTYtODEyYy1mYjc1Y2VlMjY2YTQiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9hZGQyOTQ4OS03MjY5LTQxZjQtODg0MS1iNjNjOTU1NjQ0MjAvIiwiaWF0IjoxNDE5MjY4NTIwLCJuYmYiOjE0MTkyNjg1MjAsImV4cCI6MTQxOTI3MjQyMCwidmVyIjoiMS4wIiwidGlkIjoiYWRkMjk0ODktNzI2OS00MWY0LTg4NDEtYjYzYzk1NTY0NDIwIiwiYW1yIjpbInB3ZCJdLCJvaWQiOiI4MDAyNzk2NC1jZDcwLTRmMmMtOTcwMC0yYzFhNmRiNTZlZjYiLCJ1cG4iOiJib2JAY3lyYW5vLm9ubWljcm9zb2Z0LmNvbSIsInVuaXF1ZV9uYW1lIjoiYm9iQGN5cmFuby5vbm1pY3Jvc29mdC5jb20iLCJzdWIiOiJoMnh6WVczbWdUWmZad3B3T1d4QTFZcDJ0am9Xc0ZxOWlGa1AxTjJRUndrIiwiZmFtaWx5X25hbWUiOiJDeXJhbm8iLCJnaXZlbl9uYW1lIjoiQm9iIiwibm9uY2UiOiI2MzU1NDg2NTYxMzk1MzcwNDYuT0RZMU5EWTRaRGt0T1RNNE55MDBaR0l5TFRnMk1EQXRZakkxTWpNME9HVXhOVGRtTkRVek5USXlNR0V0WldJd1lTMDBNMkpoTFRobE4yUXRaVFEwWWpJMk1tRTFaak16IiwiY19oYXNoIjoiMXVHNEVfWWdYcTZkVUctTExzeGtjQSIsInB3ZF9leHAiOiI1MzQ1MDIiLCJwd2RfdXJsIjoiaHR0cHM6Ly9wb3J0YWwubWljcm9zb2Z0b25saW5lLmNvbS9DaGFuZ2VQYXNzd29yZC5hc3B4In0.juYFCrJbDPwqZeNmR9XiFRh3iobf76fKHrE4ViqELbuz0cHhAWzntR_kshoyCCBx5Q_uQcAYnrUyvHuXsQoLqUHot6Ksnlc7uUFAeWBgSIAIRX2np-fCn0_CzgwgvBu9KOUV27uu28tEPBfxHCmU9CCH41aSLoGzGBiorQ_ss0LO3ZapLiB5T2yRaJh-ZCSuGbjTCvMAmUFx4I2rvHSNaJQOqUT02EjkHzU3qAJuYSH1Z_G36Bfyiixpbyq8Txewqaot0sHCwOrBY9yjTx8Ijrnbn7_xQHV2LyvUnSxZjL0bVUZRmWyXJ0st7Cjd9intcMYb60XSmkZwLfKzMtBY2Q";
        public static string ValidJweDirect =       @"eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2Iiwia2lkIjoiRGVmYXVsdFN5bW1ldHJpY1NlY3VyaXR5S2V5XzI1NiIsInR5cCI6IkpXVCJ9..tAYQP0lh6N8FPkuKzb0A6Q.QLHEhzxxL5q05YE6Wuo-eHmvGPGvcexii-fi-SAXI0UeR-TTaFtxgjsOZ8-d4FEujB2InS6X5CLVX6_x216Ze5lGPX5XegKq6d7mwlAmMqqtz7yHnFzSi_rZr_0uBbOlDmdRC98ilNVNjORy1u-6V_aSZTdUGjWNg3Y1gP9q_OUl-Tu9QLWvNI1cAP_PRhLn46b9RpZwUYCCF9a4qpkAZOje5X77aoy55Yw3m-HkTjR6t1j2mU1p0KK3ScW7Lrv3SeQllG5yHEdBwg8E9y9ssJyEaT5GNRQHm0n6wHIkc478pmHe1ME7wt7bX58mqOprKY-bYB1HNnW3PgXfaL-AUXxlCyk7LjLcmd2j0NUBKx17taBERSFF1sH07ynXZTlP0NYZGAr_ChrO-yY1TwMZbdoCOoUKBqcMpv7yjebhq4_50PhyLka7ZfJ5s4quYijfzlBavdnMrOVeoRwJF-kpnVRJJDdpmHTVJJuoKSnHAcDIZ5N17z6SDiKzm6TZEtaQSjOtJOn5hrRAuI4av4nzTAYXc9YqBjlozLQQ9P2SzRJh1wpKFP7XqSBVW2DvlQ_GTZS_qXTlI-fv2NG4dZpno34d-WFlcyJsw2uDeR7mi2ej8rYljH_svWK2cyZXXEyoHUzI8rebzoIS61LoFeMMVtAgpXYSsQUdq4GqPhxcI21_JTGvwJpSTXGtW5s44rwfF3jeZ7KJLSIM14A0OlzNlJX0l-UJ250bmaQ5WNxc6SemvofO4AokC-BuGtNlkM9DvJJ_YvmBLH_BGaK6ENOoyAtJ2fHrcUwKraKc_YErOLBuCrwTHe9ScCp5MHcZZoX3UmNXsX5iWL9qXziXHvlWSIRaYTJwPVa021F0B-Rnccibr_bF7PaXHnR0GIS1MvHJ.SnoMW7P4IcWMt78st0WDFQ";
        public static string ValidJweDirect2 =      @"eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2Iiwia2lkIjoiRGVmYXVsdFN5bW1ldHJpY1NlY3VyaXR5S2V5XzI1NiIsInR5cCI6IkpXVCJ9..NrwIBN5FBvFaWFfK5rcazw.ufmYVG_7gYQToytSrczL4KMWm5LoZ-DWe4Zbos4s3IYBwN4mhxI6tlHj7M4jlU8XYfoT2xq_q3cMaGPIcAHDLrgB1EIfI0L8cWOTTxAnPfAuBbaJuuHCI2OnpCMIKUKKQ-uiZm0MWIWuGUg6pFHtzxysis_grKLLnkssFFPvmxr1ysd01fNlealKoSwqO99OiLIz9hlHnLeqF3c3C07r7tuB-xT95ixtZQBfXI7iCUHlhU30T9dG-m6SwUh0LvYPGZnC987eyUpSNJ-C0bnqEig9KY_cmB9yuQ1UvoWYQzDuRDATOF9UK3s7J32mcGSqehDnsGGFwzTHdwaV7KA1dYRmm85NWupNTLdfmvTNPDRCPj5VPWzNpKfee7MMEZn81J1695N8oVBJe4qwjK2d4gqA3A1mxpgZcK9C1jc4aU2OTkVbjJstPKatqJ-kdl3-L4TJStBcO6LS9nLcO8DvGVXX0XBc2ZBliwLL2mMZ2yIXiwuOtF3UYe8jXCr9nxTX2yLVMtsOFpk-8j3cNxFtfnU8yCE84saldtwDy9X896Caa2kSBRR-tdUSqYA3Zp0xCw3XQ6zuYriFOHxTR3AmwWrxxeaGF1hbkJfoBZriPpD-qvPTiWHUPOsJYDWJ8iY13G2-CQ2lcKXP_CZKi0L9fdXhx8qccBt89DUCUuMo9FjQLS3Mh2HvbkYVouSwUnHnMpnKehaiH0O_bQR0pRCdV6sfVuMPMsO4tARK1rUlIjROlZBKyLyNeR_YFZ50U1SSaAxnsnsk2MucTt5lfOUjFhXko-qWwHnib61lFJlnTMR-ntImTIj1XyMUcf2RZ6HdlZbvXjA92fRaQ2-3Mc7eeJscT3mW4W4Yoe3BccHEmyzSCHVNsskCF0AULjSs-bcE.6gXNNIY0ZAXi6tSbz6l6Fg";
        public static string InvalidJweDirect =     @"0.1.2.3.4";
        public static string InvalidJweDirect2 =    @"eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2Iiwia2lkIjoiRGVmYXVsdFN5bW1ldHJpY1NlY3VyaXR5S2V5XzI1NiIsInR5cCI6IkpXVCJ9..llBJ2JnrWTE3ST87flwQ_Q.Ws5IYkTFiKbo52id0RaPe4ct1JM9D0zQU9YquWWiu6TggSo6-Labom-jUHoEvCRXbYWzGwZkaW3FwJwWTwsZNMO82VVi1F5tylHNNdT3CY4VvJB6oKdTyLy85jyjBMOJUjcniAFVOc22t_n7ixaWHpajx3x04T1J_ckyVFeA54NuPfk_Vsl-ckaomoLK1BQlfgDpoQ0du2xNgSf2KwtiLLYLZFHkB17swWg6Tj4i0a3cZlLhGLFY9eJBnOBMCE64ULHoDj_6gOY0QkVvyP1hMzFeLoBGtLHLHkxHngOgLpcPmiGjVUVzLppQ6kRePeVrmSndYSeyZU_8yqWPBdq9-mGahlfsOD1gF5CTY-CrpONLyFcnbMsa9Zucg6WpYJ8jeCP0LJcQhcFS8OyRW6utTg6ziZmnjgz1Kfe5z99In2k4iCNI2xTs_JgdXNbMn5P4FUSyQkKTEVZwiq6M9o0uhglDgRlpGgSXiAE28VYzezsAAjAVZ3NshRX-OA5dPS8vCgQbAm5nRTJV-JgoHQTlCEgczGpbgLmGGlgR1KrsZfA_sq33WM5jgc5h8rlIWimx2SdXQA6Gu8hfaUSxyw4JmosVPZkfmUL0AfrMdoWuzsKQZJd2KLkAcHKZputZDDB4dpbVtPWm6n8PO-QpLlzHqX-Dm7y7evZObtJYEYHvLnc_oeN0nR-tFKWUlEdgoaDRn8sBPHmXf8XbJ8PKA3xQRbLjjqw2-zmcH5VPOVptnZKctMBkoWDfH0tnr0D5-97ISNn35yUrI91Xq2S4V_fF12GwVH1m0jNHqBh8Fbliybfl4skBzTuH4c2wrYy4QB7LVhlWwEw64dL-cIUROqaMv0SqF34nxTjRkKo6uZg1JCRkwwUEjisTFDFZAEjbgy2c.ysPkppN3Q6Ehxq6_4ESMbg";
        public static string InvalidJwe =           @"0.1.2.3";
        public static string InvalidJwe2 =          @"0.1.2.3.4.5";
        public static string InvalidJwe3 =          @"0.1.2.3.4*&";
    }

    public static class Saml2SignedTokens
    {
        public static readonly string AAD_GotJWT = @"<Assertion ID='_775bef7d-0ba5-4df5-98f8-ec57f2b0cbef' IssueInstant='2013-04-11T20:02:45.903Z' Version='2.0' xmlns='urn:oasis:names:tc:SAML:2.0:assertion'><Issuer>https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/</Issuer><ds:Signature xmlns:ds='http://www.w3.org/2000/09/xmldsig#'><ds:SignedInfo><ds:CanonicalizationMethod Algorithm='http://www.w3.org/2001/10/xml-exc-c14n#' /><ds:SignatureMethod Algorithm='http://www.w3.org/2001/04/xmldsig-more#rsa-sha256' /><ds:Reference URI='#_775bef7d-0ba5-4df5-98f8-ec57f2b0cbef'><ds:Transforms><ds:Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature' /><ds:Transform Algorithm='http://www.w3.org/2001/10/xml-exc-c14n#' /></ds:Transforms><ds:DigestMethod Algorithm='http://www.w3.org/2001/04/xmlenc#sha256' /><ds:DigestValue>m1jXR5LORy7H+t+axtNauGnJakaKehvy0JhMZomZFrU=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>IhaRBWAG/QeskVPO5Qa3EGN0A/bJ8qPqmJnuhox5F3aAGgqeoxbMhdwDMuQ7GC/7ScYDtYnAKATV6xP0nKTzwlqj1ltDEDN9scDuJIotwnV+xm4PCqlIeSZBHfyhIDp0rlhZDmhOnNy6+53FqVWMo7aSE1FdMCEh2aBgTRLH4jBkx5pKV0kQOX9D0Hq2GaV0LaXjcguADKxPpb1Gl+g8g/H/79n96fHAPITA8WygZjv1Satow87N8WK49n9qZJBWqmMjaVix6oFHPMOkp/qWE7ZhWKS1ANU2AwLMvZ8AH+RDwhWTWHnLCq2duD4XlCTRiY8juIn+zgJ51sfWQRYa2w==</ds:SignatureValue><KeyInfo xmlns='http://www.w3.org/2000/09/xmldsig#'><X509Data><X509Certificate>MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng</X509Certificate></X509Data></KeyInfo></ds:Signature><Subject><NameID>X3hlFkILohbCj9c2Iib1NJg7e3hGhVsJdHuA7WRABp4</NameID><SubjectConfirmation Method='urn:oasis:names:tc:SAML:2.0:cm:bearer' /></Subject><Conditions NotBefore='2013-04-11T20:02:45.840Z' NotOnOrAfter='2013-04-12T08:02:45.840Z'><AudienceRestriction><Audience>https://localhost:44300/</Audience></AudienceRestriction></Conditions><AttributeStatement><Attribute Name='http://schemas.microsoft.com/identity/claims/objectidentifier'><AttributeValue>580e531c-87a4-415a-9f9d-6af51e2e1948</AttributeValue></Attribute><Attribute Name='http://schemas.microsoft.com/identity/claims/tenantid'><AttributeValue>d062b2b0-9aca-4ff7-b32a-ba47231a4002</AttributeValue></Attribute><Attribute Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'><AttributeValue>Got</AttributeValue></Attribute><Attribute Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'><AttributeValue>GotJwt@GotJwt.onmicrosoft.com</AttributeValue></Attribute><Attribute Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname'><AttributeValue>Jwt</AttributeValue></Attribute><Attribute Name='http://schemas.microsoft.com/identity/claims/identityprovider'><AttributeValue>https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/</AttributeValue></Attribute></AttributeStatement><AuthnStatement AuthnInstant='2013-04-11T20:02:45.000Z'><AuthnContext><AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef></AuthnContext></AuthnStatement></Assertion>";
    }

    public static class JsonClaims
    {
        public static Dictionary<string, object> ClaimSourcesAsDictionary
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "src1",
                        new Dictionary<string,string>
                        {
                            { "endpoint", "https://graph.windows.net/5803816d-c4ab-4601-a128-e2576e5d6910/users/0c9545d0-a670-4628-8c1f-e90618a3b940/getMemberObjects"},
                            { "access_token", "ksj3n283dke"}
                        }
                    },
                    {
                        "src2",
                        new Dictionary<string,string>
                        {
                            { "endpoint2", "https://graph.windows.net/5803816d-c4ab-4601-a128-e2576e5d6910/users/0c9545d0-a670-4628-8c1f-e90618a3b940/getMemberObjects"},
                            { "access_token2", "ksj3n283dke"}
                        }
                    }

                };
            }
        }

        public static Dictionary<string, object> ClaimNamesAsDictionary
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "groups",
                        "src1"
                    },
                    {
                        "groups2",
                        "src2"
                    }
                };
            }
        }

        public static ClaimsIdentity ClaimsIdentityDistributedClaims(string issuer, string authType, Dictionary<string, object> claimSources, Dictionary<string, object> claimNames)
        {
            var identity = new ClaimsIdentity(authType);
            var claimValue = BuildClaimValue(claimSources);
            identity.AddClaim(new Claim("_claim_sources", claimValue, JsonClaimValueTypes.Json, issuer, issuer, identity));
            claimValue = BuildClaimValue(claimNames);
            identity.AddClaim(new Claim("_claim_names", claimValue, JsonClaimValueTypes.Json, issuer, issuer, identity));
            identity.AddClaim(new Claim("iss", issuer, ClaimValueTypes.String, issuer));
            return identity;
        }

        private static string BuildClaimValue(Dictionary<string, object> claimSources)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            bool first = true;
            foreach (var kv in claimSources)
            {
                if (!first)
                    sb.Append(",");
                sb.Append(@"""" + kv.Key + @""":" + JsonConvert.SerializeObject(kv.Value));
                first = false;
            }

            sb.Append("}");

            return sb.ToString();
        }
    }
}
