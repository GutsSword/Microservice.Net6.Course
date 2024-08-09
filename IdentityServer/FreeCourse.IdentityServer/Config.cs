// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_catolog")
                {
                    Scopes={"CatologFullPermission"},
                },
                new ApiResource("resource_photo_stock")
                {
                    Scopes={ "PhotoStockFullPermission" }
                },
                 new ApiResource("resource_basket")
                {
                    Scopes={ "BasketFullPermission" }
                },
                 new ApiResource("resource_discount")
                {
                    Scopes={"DiscountFullPermission"},
                },
                 new ApiResource("resouce_order")
                {
                    Scopes={"OrderFullPermission"},
                },
                 new ApiResource("resource_fake_payment")
                {
                    Scopes={"FakePaymentFullPermission"},
                },
                 new ApiResource("resource_gateway")
                {
                    Scopes={"GatewayFullPermission"},
                },
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Email(),  // Email Claim
                new IdentityResources.OpenId(),  // OpenId mutlaka olmak zorunda. sub Claim
                new IdentityResources.Profile(),  // Adres, isim, cinsiyet vs Claim
                new IdentityResource()
                {
                    Name="roles",
                    DisplayName="Roles",
                    Description="Kullanıcı Rolleri",
                    UserClaims = new[]
                    {
                        "role"
                    },
                },
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("CatologFullPermission","Catolog Api için Full Erişim."),
                new ApiScope("PhotoStockFullPermission","PhotoStock Api için Full Erişim."),
                new ApiScope("BasketFullPermission","Basket Api için Full Erişim."),
                new ApiScope("DiscountFullPermission","Discount Api için Full Erişim."),
                new ApiScope("OrderFullPermission","Order Api için Full Erişim."),
                new ApiScope("FakePaymentFullPermission","FakePayment Api için Full Erişim."),
                new ApiScope("GatewayFullPermission","Gateway Api için Full Erişim."),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),  // "IdentityServerApi"
            };

        // Tokeni almak isteyen yer. (MVC , Angular Front App vs)
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets =
                    {
                        new Secret("0oa2hl2inow5Uqc6c357".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes=
                    {
                        "CatologFullPermission" ,
                        "PhotoStockFullPermission",
                        "GatewayFullPermission",
                        IdentityServerConstants.LocalApi.ScopeName,
                    },
                },
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    ClientSecrets =
                    {
                        new Secret("0oa2hl2inow5Uqc6c357".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true, // Refresh Tokene izin ver.
                    AllowedScopes=
                    {
                        "CatologFullPermission" ,
                        "PhotoStockFullPermission",
                        "BasketFullPermission",
                        "OrderFullPermission",
                        "GatewayFullPermission",
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,  //Refresh Token
                        "roles",
                    },
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,  // Absolute gün artışını engeller.
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse, // Token tekrar kullanılabilir. 
                },
                new Client
                {
                    ClientName = "Token Exchange Client",
                    ClientId = "TokenExchangeClient",
                    ClientSecrets =
                    {
                        new Secret("0oa2hl2inow5Uqc6c357".Sha256())
                    },
                    AllowedGrantTypes = new []{ "urn:ietf:params:oauth:grant-type:token-exchange" },
                    AllowedScopes=
                    {
                        "FakePaymentFullPermission",
                        "DiscountFullPermission",
                        IdentityServerConstants.StandardScopes.OpenId,
                    },
                },

            };
    }
}