using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum CurrencyPairEnum
    {
        [Metadata("EUR-USD", "EURUSD", resourceType: typeof(CurrenciesResources))]
        EURUSD = 1,

        [Metadata("GBP-USD", "GBPUSD", resourceType: typeof(CurrenciesResources))]
        GBPUSD = 2
    }

    public enum TimeframeEnum
    {
        [Metadata("M1", "M1", valueKey: "1", resourceType: typeof(EnumResources))]
        M1 = 1,

        [Metadata("M5", "M5", valueKey: "5", resourceType: typeof(EnumResources))]
        M5 = 2,

        [Metadata("M15", "M15", valueKey: "15", resourceType: typeof(EnumResources))]
        M15 = 3,

        [Metadata("M30", "M30", valueKey: "30", resourceType: typeof(EnumResources))]
        M30 = 4,

        [Metadata("H1", "H1", valueKey: "16385", resourceType: typeof(EnumResources))]
        H1 = 5,

        [Metadata("H4", "H4", valueKey: "16388", resourceType: typeof(EnumResources))]
        H4 = 6,

        [Metadata("D1", "D1", valueKey: "16408", resourceType: typeof(EnumResources))]
        D1 = 7,

        [Metadata("W1", "W1", valueKey: "32769", resourceType: typeof(EnumResources))]
        W1 = 8,

        [Metadata("MN1", "MN1", valueKey: "49153", resourceType: typeof(EnumResources))]
        MN1 = 9
    }

    public enum CurrencySpreadEnum
    {
        [Metadata("One", "One", valueKey: "1", resourceType: typeof(EnumResources))]
        One = 1,

        [Metadata("Two", "Two", valueKey: "2", resourceType: typeof(EnumResources))]
        Two = 2,

        [Metadata("Three", "Three", valueKey: "3", resourceType: typeof(EnumResources))]
        Three = 3,

        [Metadata("Four", "Four", valueKey: "4", resourceType: typeof(EnumResources))]
        Four = 4,

        [Metadata("Five", "Five", valueKey: "5", resourceType: typeof(EnumResources))]
        Five = 5,

        [Metadata("Six", "Six", valueKey: "6", resourceType: typeof(EnumResources))]
        Six = 6,

        [Metadata("Seven", "Seven", valueKey: "7", resourceType: typeof(EnumResources))]
        Seven = 7,

        [Metadata("Eight", "Eight", valueKey: "8", resourceType: typeof(EnumResources))]
        Eight = 8,

        [Metadata("Nine", "Nine", valueKey: "9", resourceType: typeof(EnumResources))]
        Nine = 9,

        [Metadata("Ten", "Ten", valueKey: "10", resourceType: typeof(EnumResources))]
        Ten = 10,

        [Metadata("Eleven", "Eleven", valueKey: "11", resourceType: typeof(EnumResources))]
        Eleven = 11,

        [Metadata("Twelve", "Twelve", valueKey: "12", resourceType: typeof(EnumResources))]
        Twelve = 12,

        [Metadata("Thirteen", "Thirteen", valueKey: "13", resourceType: typeof(EnumResources))]
        Thirteen = 13,

        [Metadata("Fourteen", "Fourteen", valueKey: "14", resourceType: typeof(EnumResources))]
        Fourteen = 14,

        [Metadata("Fifteen", "Fifteen", valueKey: "15", resourceType: typeof(EnumResources))]
        Fifteen = 15,
    }

    public enum CurrencyEnum
    {
        [Metadata("AFN", resourceType: typeof(CurrenciesResources))]
        AFN = 1,

        [Metadata("AFA", resourceType: typeof(CurrenciesResources))]
        AFA = 2,

        [Metadata("ALL", resourceType: typeof(CurrenciesResources))]
        ALL = 3,

        [Metadata("DZD", resourceType: typeof(CurrenciesResources))]
        DZD = 4,

        [Metadata("ADF", resourceType: typeof(CurrenciesResources))]
        ADF = 5,

        [Metadata("ADP", resourceType: typeof(CurrenciesResources))]
        ADP = 6,

        [Metadata("AOA", resourceType: typeof(CurrenciesResources))]
        AOA = 7,

        [Metadata("AON", resourceType: typeof(CurrenciesResources))]
        AON = 8,

        [Metadata("ARS", resourceType: typeof(CurrenciesResources))]
        ARS = 9,

        [Metadata("AMD", resourceType: typeof(CurrenciesResources))]
        AMD = 10,

        [Metadata("AWG", resourceType: typeof(CurrenciesResources))]
        AWG = 11,

        [Metadata("AUD", resourceType: typeof(CurrenciesResources))]
        AUD = 12,

        [Metadata("ATS", resourceType: typeof(CurrenciesResources))]
        ATS = 13,

        [Metadata("AZM", resourceType: typeof(CurrenciesResources))]
        AZM = 14,

        [Metadata("AZN", resourceType: typeof(CurrenciesResources))]
        AZN = 15,

        [Metadata("BSD", resourceType: typeof(CurrenciesResources))]
        BSD = 16,

        [Metadata("BHD", resourceType: typeof(CurrenciesResources))]
        BHD = 17,

        [Metadata("BDT", resourceType: typeof(CurrenciesResources))]
        BDT = 18,

        [Metadata("BBD", resourceType: typeof(CurrenciesResources))]
        BBD = 19,

        [Metadata("BEF", resourceType: typeof(CurrenciesResources))]
        BEF = 20,

        [Metadata("BZD", resourceType: typeof(CurrenciesResources))]
        BZD = 21,

        [Metadata("BMD", resourceType: typeof(CurrenciesResources))]
        BMD = 22,

        [Metadata("BTN", resourceType: typeof(CurrenciesResources))]
        BTN = 23,

        [Metadata("BOB", resourceType: typeof(CurrenciesResources))]
        BOB = 24,

        [Metadata("BAM", resourceType: typeof(CurrenciesResources))]
        BAM = 25,

        [Metadata("BWP", resourceType: typeof(CurrenciesResources))]
        BWP = 26,

        [Metadata("BRL", resourceType: typeof(CurrenciesResources))]
        BRL = 27,

        [Metadata("GBP", resourceType: typeof(CurrenciesResources))]
        GBP = 28,

        [Metadata("BND", resourceType: typeof(CurrenciesResources))]
        BND = 29,

        [Metadata("BGN", resourceType: typeof(CurrenciesResources))]
        BGN = 30,

        [Metadata("BGL", resourceType: typeof(CurrenciesResources))]
        BGL = 31,

        [Metadata("BIF", resourceType: typeof(CurrenciesResources))]
        BIF = 32,

        [Metadata("BYR", resourceType: typeof(CurrenciesResources))]
        BYR = 33,

        [Metadata("XOF", resourceType: typeof(CurrenciesResources))]
        XOF = 34,

        [Metadata("XAF", resourceType: typeof(CurrenciesResources))]
        XAF = 35,

        [Metadata("XPF", resourceType: typeof(CurrenciesResources))]
        XPF = 36,

        [Metadata("KHR", resourceType: typeof(CurrenciesResources))]
        KHR = 37,

        [Metadata("CAD", resourceType: typeof(CurrenciesResources))]
        CAD = 38,

        [Metadata("CVE", resourceType: typeof(CurrenciesResources))]
        CVE = 39,

        [Metadata("KYD", resourceType: typeof(CurrenciesResources))]
        KYD = 40,

        [Metadata("CLP", resourceType: typeof(CurrenciesResources))]
        CLP = 41,

        [Metadata("CNY", resourceType: typeof(CurrenciesResources))]
        CNY = 42,

        [Metadata("COP", resourceType: typeof(CurrenciesResources))]
        COP = 43,

        [Metadata("KMF", resourceType: typeof(CurrenciesResources))]
        KMF = 44,

        [Metadata("CDF", resourceType: typeof(CurrenciesResources))]
        CDF = 45,

        [Metadata("CRC", resourceType: typeof(CurrenciesResources))]
        CRC = 46,

        [Metadata("HRK", resourceType: typeof(CurrenciesResources))]
        HRK = 47,

        [Metadata("CUC", resourceType: typeof(CurrenciesResources))]
        CUC = 48,

        [Metadata("CUP", resourceType: typeof(CurrenciesResources))]
        CUP = 49,

        [Metadata("CYP", resourceType: typeof(CurrenciesResources))]
        CYP = 50,

        [Metadata("CZK", resourceType: typeof(CurrenciesResources))]
        CZK = 51,

        [Metadata("DKK", resourceType: typeof(CurrenciesResources))]
        DKK = 52,

        [Metadata("DJF", resourceType: typeof(CurrenciesResources))]
        DJF = 53,

        [Metadata("DOP", resourceType: typeof(CurrenciesResources))]
        DOP = 54,

        [Metadata("NLG", resourceType: typeof(CurrenciesResources))]
        NLG = 55,

        [Metadata("XCD", resourceType: typeof(CurrenciesResources))]
        XCD = 56,

        [Metadata("XEU", resourceType: typeof(CurrenciesResources))]
        XEU = 57,

        [Metadata("ECS", resourceType: typeof(CurrenciesResources))]
        ECS = 58,

        [Metadata("EGP", resourceType: typeof(CurrenciesResources))]
        EGP = 59,

        [Metadata("SVC", resourceType: typeof(CurrenciesResources))]
        SVC = 60,

        [Metadata("EEK", resourceType: typeof(CurrenciesResources))]
        EEK = 61,

        [Metadata("ETB", resourceType: typeof(CurrenciesResources))]
        ETB = 62,

        [Metadata("EUR", resourceType: typeof(CurrenciesResources))]
        EUR = 63,

        [Metadata("FKP", resourceType: typeof(CurrenciesResources))]
        FKP = 64,

        [Metadata("FJD", resourceType: typeof(CurrenciesResources))]
        FJD = 65,

        [Metadata("FIM", resourceType: typeof(CurrenciesResources))]
        FIM = 66,

        [Metadata("FRF", resourceType: typeof(CurrenciesResources))]
        FRF = 67,

        [Metadata("GMD", resourceType: typeof(CurrenciesResources))]
        GMD = 68,

        [Metadata("GEL", resourceType: typeof(CurrenciesResources))]
        GEL = 69,

        [Metadata("DEM", resourceType: typeof(CurrenciesResources))]
        DEM = 70,

        [Metadata("GHC", resourceType: typeof(CurrenciesResources))]
        GHC = 71,

        [Metadata("GHS", resourceType: typeof(CurrenciesResources))]
        GHS = 72,

        [Metadata("GIP", resourceType: typeof(CurrenciesResources))]
        GIP = 73,

        [Metadata("XAU", resourceType: typeof(CurrenciesResources))]
        XAU = 74,

        [Metadata("GRD", resourceType: typeof(CurrenciesResources))]
        GRD = 75,

        [Metadata("GTQ", resourceType: typeof(CurrenciesResources))]
        GTQ = 76,

        [Metadata("GNF", resourceType: typeof(CurrenciesResources))]
        GNF = 77,

        [Metadata("GYD", resourceType: typeof(CurrenciesResources))]
        GYD = 78,

        [Metadata("HTG", resourceType: typeof(CurrenciesResources))]
        HTG = 79,

        [Metadata("HNL", resourceType: typeof(CurrenciesResources))]
        HNL = 80,

        [Metadata("HKD", resourceType: typeof(CurrenciesResources))]
        HKD = 81,

        [Metadata("HUF", resourceType: typeof(CurrenciesResources))]
        HUF = 82,

        [Metadata("ISK", resourceType: typeof(CurrenciesResources))]
        ISK = 83,

        [Metadata("INR", resourceType: typeof(CurrenciesResources))]
        INR = 84,

        [Metadata("IDR", resourceType: typeof(CurrenciesResources))]
        IDR = 85,

        [Metadata("IRR", resourceType: typeof(CurrenciesResources))]
        IRR = 86,

        [Metadata("IQD", resourceType: typeof(CurrenciesResources))]
        IQD = 87,

        [Metadata("IEP", resourceType: typeof(CurrenciesResources))]
        IEP = 88,

        [Metadata("ILS", resourceType: typeof(CurrenciesResources))]
        ILS = 89,

        [Metadata("ITL", resourceType: typeof(CurrenciesResources))]
        ITL = 90,

        [Metadata("JMD", resourceType: typeof(CurrenciesResources))]
        JMD = 91,

        [Metadata("JPY", resourceType: typeof(CurrenciesResources))]
        JPY = 92,

        [Metadata("JOD", resourceType: typeof(CurrenciesResources))]
        JOD = 93,

        [Metadata("KHR", resourceType: typeof(CurrenciesResources))]
        KKHR = 94,

        [Metadata("KZT", resourceType: typeof(CurrenciesResources))]
        KZT = 95,

        [Metadata("KES", resourceType: typeof(CurrenciesResources))]
        KES = 96,

        [Metadata("KRW", resourceType: typeof(CurrenciesResources))]
        KRW = 97,

        [Metadata("KWD", resourceType: typeof(CurrenciesResources))]
        KWD = 98,

        [Metadata("KGS", resourceType: typeof(CurrenciesResources))]
        KGS = 99,

        [Metadata("LAK", resourceType: typeof(CurrenciesResources))]
        LAK = 100,

        [Metadata("LVL", resourceType: typeof(CurrenciesResources))]
        LVL = 101,

        [Metadata("LBP", resourceType: typeof(CurrenciesResources))]
        LBP = 102,

        [Metadata("LSL", resourceType: typeof(CurrenciesResources))]
        LSL = 103,

        [Metadata("LRD", resourceType: typeof(CurrenciesResources))]
        LRD = 104,

        [Metadata("LYD", resourceType: typeof(CurrenciesResources))]
        LYD = 105,

        [Metadata("LTL", resourceType: typeof(CurrenciesResources))]
        LTL = 106,

        [Metadata("LUF", resourceType: typeof(CurrenciesResources))]
        LUF = 107,

        [Metadata("MOP", resourceType: typeof(CurrenciesResources))]
        MOP = 108,

        [Metadata("MKD", resourceType: typeof(CurrenciesResources))]
        MKD = 109,

        [Metadata("MGF", resourceType: typeof(CurrenciesResources))]
        MGF = 110,

        [Metadata("MWK", resourceType: typeof(CurrenciesResources))]
        MWK = 111,

        [Metadata("MGA", resourceType: typeof(CurrenciesResources))]
        MGA = 112,

        [Metadata("MYR", resourceType: typeof(CurrenciesResources))]
        MYR = 113,

        [Metadata("MVR", resourceType: typeof(CurrenciesResources))]
        MVR = 114,

        [Metadata("MTL", resourceType: typeof(CurrenciesResources))]
        MTL = 115,

        [Metadata("MRO", resourceType: typeof(CurrenciesResources))]
        MRO = 116,

        [Metadata("MUR", resourceType: typeof(CurrenciesResources))]
        MUR = 117,

        [Metadata("MXN", resourceType: typeof(CurrenciesResources))]
        MXN = 118,

        [Metadata("MDL", resourceType: typeof(CurrenciesResources))]
        MDL = 119,

        [Metadata("MNT", resourceType: typeof(CurrenciesResources))]
        MNT = 120,

        [Metadata("MAD", resourceType: typeof(CurrenciesResources))]
        MAD = 121,

        [Metadata("MZM", resourceType: typeof(CurrenciesResources))]
        MZM = 122,

        [Metadata("MZN", resourceType: typeof(CurrenciesResources))]
        MZN = 123,

        [Metadata("MMK", resourceType: typeof(CurrenciesResources))]
        MMK = 124,

        [Metadata("ANG", resourceType: typeof(CurrenciesResources))]
        ANG = 125,

        [Metadata("NAD", resourceType: typeof(CurrenciesResources))]
        NAD = 126,

        [Metadata("NPR", resourceType: typeof(CurrenciesResources))]
        NPR = 127,

        [Metadata("NLG", resourceType: typeof(CurrenciesResources))]
        NLGG = 128,

        [Metadata("NZD", resourceType: typeof(CurrenciesResources))]
        NZD = 129,

        [Metadata("NIO", resourceType: typeof(CurrenciesResources))]
        NIO = 130,

        [Metadata("NGN", resourceType: typeof(CurrenciesResources))]
        NGN = 131,

        [Metadata("KPW", resourceType: typeof(CurrenciesResources))]
        KPW = 132,

        [Metadata("NOK", resourceType: typeof(CurrenciesResources))]
        NOK = 133,

        [Metadata("OMR", resourceType: typeof(CurrenciesResources))]
        OMR = 134,

        [Metadata("PKR", resourceType: typeof(CurrenciesResources))]
        PKR = 135,

        [Metadata("XPD", resourceType: typeof(CurrenciesResources))]
        XPD = 136,

        [Metadata("PAB", resourceType: typeof(CurrenciesResources))]
        PAB = 137,

        [Metadata("PGK", resourceType: typeof(CurrenciesResources))]
        PGK = 138,

        [Metadata("PYG", resourceType: typeof(CurrenciesResources))]
        PYG = 139,

        [Metadata("PEN", resourceType: typeof(CurrenciesResources))]
        PEN = 140,

        [Metadata("PHP", resourceType: typeof(CurrenciesResources))]
        PHP = 141,

        [Metadata("XPT", resourceType: typeof(CurrenciesResources))]
        XPT = 142,

        [Metadata("Mexico", resourceType: typeof(CurrenciesResources))]
        Mexico = 143,

        [Metadata("PLN", resourceType: typeof(CurrenciesResources))]
        PLN = 144,

        [Metadata("PTE", resourceType: typeof(CurrenciesResources))]
        PTE = 145,

        [Metadata("GBP", resourceType: typeof(CurrenciesResources))]
        GGBP = 146,

        [Metadata("ROL", resourceType: typeof(CurrenciesResources))]
        ROL = 147,

        [Metadata("RON", resourceType: typeof(CurrenciesResources))]
        RON = 148,

        [Metadata("RUB", resourceType: typeof(CurrenciesResources))]
        RUB = 149,

        [Metadata("RWF", resourceType: typeof(CurrenciesResources))]
        RWF = 150,

        [Metadata("WST", resourceType: typeof(CurrenciesResources))]
        WST = 151,

        [Metadata("STD", resourceType: typeof(CurrenciesResources))]
        STD = 152,

        [Metadata("SAR", resourceType: typeof(CurrenciesResources))]
        SAR = 153,

        [Metadata("RSD", resourceType: typeof(CurrenciesResources))]
        RSD = 154,

        [Metadata("SCR", resourceType: typeof(CurrenciesResources))]
        SCR = 155,

        [Metadata("SLL", resourceType: typeof(CurrenciesResources))]
        SLL = 156,

        [Metadata("XAG", resourceType: typeof(CurrenciesResources))]
        XAG = 157,

        [Metadata("SGD", resourceType: typeof(CurrenciesResources))]
        SGD = 158,

        [Metadata("SKK", resourceType: typeof(CurrenciesResources))]
        SKK = 159,

        [Metadata("SIT", resourceType: typeof(CurrenciesResources))]
        SIT = 160,

        [Metadata("SBD", resourceType: typeof(CurrenciesResources))]
        SBD = 161,

        [Metadata("SOS", resourceType: typeof(CurrenciesResources))]
        SOS = 162,

        [Metadata("ZAR", resourceType: typeof(CurrenciesResources))]
        ZAR = 163,

        [Metadata("ESP", resourceType: typeof(CurrenciesResources))]
        ESP = 164,

        [Metadata("LKR", resourceType: typeof(CurrenciesResources))]
        LKR = 165,

        [Metadata("SHP", resourceType: typeof(CurrenciesResources))]
        SHP = 166,

        [Metadata("SDD", resourceType: typeof(CurrenciesResources))]
        SDD = 167,

        [Metadata("SDG", resourceType: typeof(CurrenciesResources))]
        SDG = 168,

        [Metadata("SDP", resourceType: typeof(CurrenciesResources))]
        SDP = 169,

        [Metadata("SRD", resourceType: typeof(CurrenciesResources))]
        SRD = 170,

        [Metadata("SRG", resourceType: typeof(CurrenciesResources))]
        SRG = 171,

        [Metadata("SZL", resourceType: typeof(CurrenciesResources))]
        SZL = 172,

        [Metadata("SEK", resourceType: typeof(CurrenciesResources))]
        SEK = 173,

        [Metadata("CHF", resourceType: typeof(CurrenciesResources))]
        CHF = 174,

        [Metadata("SYP", resourceType: typeof(CurrenciesResources))]
        SYP = 175,

        [Metadata("TWD", resourceType: typeof(CurrenciesResources))]
        TWD = 176,

        [Metadata("TJS", resourceType: typeof(CurrenciesResources))]
        TJS = 177,

        [Metadata("TZS", resourceType: typeof(CurrenciesResources))]
        TZS = 178,

        [Metadata("THB", resourceType: typeof(CurrenciesResources))]
        THB = 179,

        [Metadata("TMM", resourceType: typeof(CurrenciesResources))]
        TMM = 180,

        [Metadata("TMT", resourceType: typeof(CurrenciesResources))]
        TMT = 181,

        [Metadata("TOP", resourceType: typeof(CurrenciesResources))]
        TOP = 182,

        [Metadata("TTD", resourceType: typeof(CurrenciesResources))]
        TTD = 183,

        [Metadata("TND", resourceType: typeof(CurrenciesResources))]
        TND = 184,

        [Metadata("TRL", resourceType: typeof(CurrenciesResources))]
        TRL = 185,

        [Metadata("TRY", resourceType: typeof(CurrenciesResources))]
        TRY = 186,

        [Metadata("UGX", resourceType: typeof(CurrenciesResources))]
        UGX = 187,

        [Metadata("UAH", resourceType: typeof(CurrenciesResources))]
        UAH = 188,

        [Metadata("UAH", resourceType: typeof(CurrenciesResources))]
        UUAH = 189,

        [Metadata("GBP", resourceType: typeof(CurrenciesResources))]
        GGGBP = 190,

        [Metadata("USD", resourceType: typeof(CurrenciesResources))]
        USD = 191,

        [Metadata("UYU", resourceType: typeof(CurrenciesResources))]
        UYU = 192,

        [Metadata("UZS", resourceType: typeof(CurrenciesResources))]
        UZS = 193,

        [Metadata("AED", resourceType: typeof(CurrenciesResources))]
        AED = 194,

        [Metadata("VUV", resourceType: typeof(CurrenciesResources))]
        VUV = 195,

        [Metadata("VEB", resourceType: typeof(CurrenciesResources))]
        VEB = 196,

        [Metadata("VEF", resourceType: typeof(CurrenciesResources))]
        VEF = 197,

        [Metadata("VND", resourceType: typeof(CurrenciesResources))]
        VND = 198,

        [Metadata("YER", resourceType: typeof(CurrenciesResources))]
        YER = 199,

        [Metadata("YUN", resourceType: typeof(CurrenciesResources))]
        YUN = 200,

        [Metadata("ZMK", resourceType: typeof(CurrenciesResources))]
        ZMK = 201,

        [Metadata("ZMW", resourceType: typeof(CurrenciesResources))]
        ZMW = 202,

        [Metadata("ZWD", resourceType: typeof(CurrenciesResources))]
        ZWD = 203
    }
}