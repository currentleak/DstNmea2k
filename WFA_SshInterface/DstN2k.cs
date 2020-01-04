using System;

namespace WindowsFormsAppKcoTestSsh
{
    struct DstDataNMEA2kStruct
    {
        public string parameter;
        public double value;
    };
    class DstN2k
    {
        //public const string CanBusId = "vcan0";
        //public const string CanBusId = "can0";

        const UInt32 PGN128259_SPEED = 0x09F50323; // 128259 = 0x1F503
        const UInt32 PGN128267_DEPTH = 0x0DF50B23;
        const UInt32 PGN128275_DISTLOG = 0x19F51323;
        const UInt32 PGN130311_WTEMP = 0x15FD0723;
        // todo a verfier les  PGN heading, Attitude...
        
        const UInt32 PGN127250_HEADING = 0x01F11223;   // 127250 = 0x1F112 --> 0x??F11223
        const UInt32 PGN127257_ATTITUDE = 0x01F11923;
        const UInt32 PGN130310_PRESSURE = 0x01FD0623;

        const uint CAN_MAX_DLEN = 8;  

        public DstDataNMEA2kStruct DstData;

        private Int32 GetPgnNumber(string MessageN2k)
        {   //   can0  09F50323   [8]  FF 09 00 FF FF 00 FF FF
            // return 09F50323 (or 89F50323 ?)
            try
            {
                MessageN2k = MessageN2k.Substring(MessageN2k.IndexOf('[') - 11, 8);
            }
            catch
            {
                return 0;
            }
            return Convert.ToInt32(MessageN2k, 16);
        }

        private string GetN2kData(string MessageN2k)
        {   //   can0  09F50323   [8]  FF 09 00 FF FF 00 FF FF
            // return FF0900FFFF00FFFF
            try
            {
                MessageN2k = MessageN2k.Substring(MessageN2k.IndexOf(']') + 3, 23);
                MessageN2k = MessageN2k.Replace(" ", "");
            }
            catch
            {
                return String.Empty;
            }
            return MessageN2k;
        }

        public string ParseNmea2kMessage(string MessageN2k)
        {
            Int32 can_id;
            can_id = GetPgnNumber(MessageN2k);

            if (can_id == PGN128259_SPEED)
            {
                DstData.parameter = "Speed";
                MessageN2k = GetN2kData(MessageN2k).Substring(4, 2) + GetN2kData(MessageN2k).Substring(2, 2);
                int s = int.Parse(MessageN2k, System.Globalization.NumberStyles.HexNumber);
                double speed = (double)s * 0.01;
                DstData.value = speed;
                return String.Format("{0:#.00}", speed);
            }
            if (can_id == PGN128267_DEPTH)
            {
                DstData.parameter = "Depth";
                MessageN2k = GetN2kData(MessageN2k).Substring(8, 2) + GetN2kData(MessageN2k).Substring(6, 2) + GetN2kData(MessageN2k).Substring(4, 2) + GetN2kData(MessageN2k).Substring(2, 2);
                int d = int.Parse(MessageN2k, System.Globalization.NumberStyles.HexNumber);
                double depth = (double)d * 0.01;
                DstData.value = depth;
                return String.Format("{0:#.00}", depth);
            }
            if (can_id == PGN128275_DISTLOG)
            {
                return Convert.ToString(PGN128275_DISTLOG);
            }
            if (can_id == PGN130311_WTEMP)
            {
                DstData.parameter = "WaterTemperature";
                MessageN2k = GetN2kData(MessageN2k).Substring(6, 2) + GetN2kData(MessageN2k).Substring(4, 2);
                int t = int.Parse(MessageN2k, System.Globalization.NumberStyles.HexNumber);
                double temperature = (double)t * 0.01 - 273.15;
                DstData.value = temperature;
                return String.Format("{0:#.00}", temperature);
            }
            if (can_id == PGN127250_HEADING)
            {
                DstData.parameter = "Heading";
                MessageN2k = GetN2kData(MessageN2k).Substring(4, 2) + GetN2kData(MessageN2k).Substring(2, 2);
                int h = int.Parse(MessageN2k, System.Globalization.NumberStyles.HexNumber);
                double heading = (double)h * 0.0001;
                heading = heading * 360 / 2 * Math.PI;
                DstData.value = heading;
                return String.Format("{0:#.0000}", heading);
            }
            if (can_id == PGN127257_ATTITUDE)
            {
                DstData.parameter = "AttitudeRoll";
                MessageN2k = GetN2kData(MessageN2k).Substring(12, 2) + GetN2kData(MessageN2k).Substring(10, 2);
                int r = int.Parse(MessageN2k, System.Globalization.NumberStyles.HexNumber);
                double roll = (double)r * 0.0001;
                roll = roll * 360 / 2 * Math.PI;
                DstData.value = roll;
                return String.Format("{0:#.0000}", roll);
            }
            if (can_id == PGN130310_PRESSURE)
            {
                DstData.parameter = "Pressure";
                MessageN2k = GetN2kData(MessageN2k).Substring(14, 2) + GetN2kData(MessageN2k).Substring(12, 2);
                int p = int.Parse(MessageN2k, System.Globalization.NumberStyles.HexNumber);
                double pressure = (double)p * 100;
                DstData.value = pressure;
                return String.Format("{0:#.00}", pressure);
            }

            return String.Empty;
        }

    }

}
