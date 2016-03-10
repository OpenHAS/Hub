using System;
using System.Text;

namespace HomeAutomation.Util
{
    /// <summary>
    /// Converts between hex strings and byte arrays.
    /// </summary>
    public static class HexConverter
    {
        // Create a character array for hexidecimal conversion.
        private static readonly char[] hexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        private static readonly byte asciiCharacter0 = 48;
        private static readonly byte asciiCharacter9 = 57;
        private static readonly byte asciiCharacterA = 65;

        /// <summary>
        /// Converts the byte array to a simple non-spaced hex string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes)
        {
            return ToHexString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Converts the byte array to a simple non-spaced hex string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes, int offset, int length)
        {
            char[] resultChars = new char[length * 2];

            int resultIndex = 0;
            for (int i = offset; i < offset + length; i++)
            {
                // Grab the top 4 bits and append the hex equivalent to the return string.
                resultChars[resultIndex] = hexChars[bytes[i] >> 4];

                // Mask off the upper 4 bits to get the rest of it.
                resultChars[resultIndex + 1] = hexChars[bytes[i] & 0x0F];

                resultIndex += 2;
            }

            return new String(resultChars);
        }

        /// <summary>
        /// Converts the byte array to a spaced hex string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToSpacedHexString(this byte[] bytes)
        {
            return ToSpacedHexString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Converts the byte array to a spaced hex string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToSpacedHexString(this byte[] bytes, int offset, int length)
        {
            char[] resultChars = new char[length * 3];

            int resultIndex = 0;
            for (int i = offset; i < offset + length; i++)
            {
                // Grab the top 4 bits and append the hex equivalent to the return string.
                resultChars[resultIndex] = hexChars[bytes[i] >> 4];

                // Mask off the upper 4 bits to get the rest of it.
                resultChars[resultIndex + 1] = hexChars[bytes[i] & 0x0F];

                resultChars[resultIndex + 2] = ' ';

                resultIndex += 3;
            }

            return new String(resultChars);
        }

        /// <summary>
        /// Parses the hex string and returns it's byte representation.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static byte[] BytesFromString(this string hexString)
        {
            byte[] characterBytes = UTF8Encoding.UTF8.GetBytes(hexString.ToUpper());
            byte[] returnValue = new byte[hexString.Length / 2];

            for (int i = 0; i < returnValue.Length; i++)
            {
                // calculate the high and low parts of the 
                byte highPart = characterBytes[i * 2] > asciiCharacter9 ? (byte)(characterBytes[i * 2] - asciiCharacterA + 10) : (byte)(characterBytes[i * 2] - asciiCharacter0);
                byte lowPart = characterBytes[i * 2 + 1] > asciiCharacter9 ? (byte)(characterBytes[i * 2 + 1] - asciiCharacterA + 10) : (byte)(characterBytes[i * 2 + 1] - asciiCharacter0);

                // store the new calculated value.
                returnValue[i] = (byte)(highPart * 16 + lowPart);
            }

            return returnValue;
        }

        /// <summary>
        /// Removes all spaces from hex string, and return the byte[] representation.
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] BytesFromSpacedString(this string hexString)
        {
            hexString = hexString.Replace(" ", String.Empty);

            return hexString.BytesFromString();
        }
    }
}
