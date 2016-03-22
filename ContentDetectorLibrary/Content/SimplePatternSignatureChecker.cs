namespace RansomwareDetection.ContentDetectorLib.Content
{
	#region Using directives.
	// ----------------------------------------------------------------------

	using System;
	using System.Text;
	using Tools;

	// ----------------------------------------------------------------------
	#endregion
	
	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// A signature checker based on simple binary pattern matching.
	/// </summary>
	internal class SimplePatternSignatureChecker :
		SignatureCheckerBase
	{
		#region Public methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SimplePatternSignatureChecker"/> class.
		/// </summary>
		/// <param name="signature">The signature.</param>
		/// <param name="signatureMode">The signature mode.</param>
		public SimplePatternSignatureChecker(
			string signature,
			SignatureMode signatureMode )
		{
			if ( signatureMode == SignatureMode.HexString )
			{
				_pattern = ConvertHexStringToBytes( signature );
			}
			else
			{
				_pattern = ConvertTextStringToBytes( signature );
			}
		}

		/// <summary>
		/// Check whether a given buffer matches the signature.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns></returns>
		public override bool MatchesSignature( 
			byte[] buffer )
		{
			return IsPatternContainedInBuffer( buffer, _pattern );
		}

		/// <summary>
		/// Converts the hex string to bytes.
		/// </summary>
		/// <param name="hexString">The hex string.</param>
		/// <returns></returns>
		private static byte[] ConvertHexStringToBytes(
			string hexString )
		{
			return HexEncoding.GetBytes( hexString );
		}

		/// <summary>
		/// Converts the text string to bytes.
		/// </summary>
		/// <param name="textString">The text string.</param>
		/// <returns></returns>
		private static byte[] ConvertTextStringToBytes(
			string textString )
		{
			return Encoding.ASCII.GetBytes( textString );
		}

		// ------------------------------------------------------------------
		#endregion

		#region Public properties.
		// ------------------------------------------------------------------

		/// <summary>
		/// Gets the minimum length of the required buffer.
		/// </summary>
		/// <value>The minimum length of the required buffer.</value>
		public override int MinimumRequiredBufferLength
		{
			get
			{
				return _pattern.Length;
			}
		}

		/// <summary>
		/// Gets the first number of bytes to read.
		/// </summary>
		/// <value>The first number of bytes to read.</value>
		public override int FirstNumberOfBytesToRead
		{
			get
			{
				return Math.Max( 100, _pattern.Length );
			}
		}

		// ------------------------------------------------------------------
		#endregion

		#region Private variables.
		// ------------------------------------------------------------------

		private readonly byte[] _pattern;

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}