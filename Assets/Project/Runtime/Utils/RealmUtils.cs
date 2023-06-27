using Realm;

namespace Utils
{
    public static class RealmUtils
    {
        public static string GetCurrentRealm(RealmType realm, SubRealmType subRealm) => GetRealm(realm) + " " + GetSubRealm(subRealm) + " Realm";
        public static string GetCurrentBodyRealm(BodyType body, SubRealmType subRealm) => GetBodyRealm(body) + " " + GetSubRealm(subRealm) + " Realm";

        public static string GetRealm(RealmType realm)
        {
            return realm switch
            {
                RealmType.QICONDENSATION => "Qi Condensation",
                RealmType.FOUNDATION => "Foundation",
                RealmType.COREFORMATION => "Core Formation",
                RealmType.SOULWANDERING => "Soul Wandering",
                RealmType.NASCENTSOUL => "Nascent Soul",
                RealmType.ASCENSION => "Ascension",
                RealmType.IMMORTAL => "Immortal",
                RealmType.PROFOUNDIMMORTAL => "Profound Immortal",
                RealmType.VENERABLE => "Venerable",
                RealmType.EMPEROR => "Emperor",
                RealmType.GOD => "God",
                RealmType.CELESTIAL => "Celestial",
                _ => string.Empty,
            };
        }

        public static string GetSubRealm(SubRealmType subRealm)
        {
            return subRealm switch
            {
                SubRealmType.ONE => "I",
                SubRealmType.TWO => "II",
                SubRealmType.THREE => "III",
                SubRealmType.FOUR => "IV",
                SubRealmType.FIVE => "V",
                SubRealmType.SIX => "VI",
                SubRealmType.SEVEN => "VII",
                SubRealmType.EIGHT => "VIII",
                SubRealmType.NINE => "IX",
                SubRealmType.TEN => "X",
                _ => string.Empty,
            };
        }

        public static string GetBodyRealm(BodyType body)
        {
            return body switch
            {
                BodyType.MORTALFLESH => "Mortal Flesh",
                BodyType.BONEREFINING => "Bone Refining",
                _ => string.Empty,
            };
        }
    }
}