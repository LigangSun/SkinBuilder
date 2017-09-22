using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Health121.Utility.Win32
{
    public class SoundPlayer
    {
        [DllImport("winmm.dll", EntryPoint="PlaySound",CharSet=CharSet.Auto)]
		private static extern int PlaySound(String pszSound, int hmod, int falgs);

		/// <summary>
		/// API Parameter Flags for PlaySound method
		/// </summary>
		public enum SND
		{
			SND_SYNC            = 0x0000  ,/* play synchronously (default) */
			SND_ASYNC           = 0x0001 , /* play asynchronously */
			SND_NODEFAULT       = 0x0002 , /* silence (!default) if sound not found */
			SND_MEMORY          = 0x0004 , /* pszSound points to a memory file */
			SND_LOOP            = 0x0008 , /* loop the sound until next sndPlaySound */
			SND_NOSTOP          = 0x0010 , /* don't stop any currently playing sound */
			SND_NOWAIT			= 0x00002000, /* don't wait if the driver is busy */
			SND_ALIAS			= 0x00010000 ,/* name is a registry alias */
			SND_ALIAS_ID		= 0x00110000, /* alias is a pre d ID */
			SND_FILENAME		= 0x00020000, /* name is file name */
			SND_RESOURCE		= 0x00040004, /* name is resource name or atom */
			SND_PURGE           = 0x0040,  /* purge non-static events for task */
			SND_APPLICATION     = 0x0080  /* look for application specific association */
		}
		/// <summary>
		/// Plays specified wav file.
		/// </summary>
		/// <param name="pszSound">Path points to the wave file.</param>
		/// <returns></returns>
		public static void PlaySound(String pszSound)
		{
			if(File.Exists(pszSound))
			{
				PlaySound(pszSound,0,(int) (SND.SND_ASYNC | SND.SND_FILENAME | SND.SND_NOWAIT));
			}

		}
		/// <summary>
		/// Play System Event stored under HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default
		/// </summary>
		/// <param name="pszSound">SystemEvent Verb</param>
		public static void PlaySoundEvent(String pszSound)
		{	
			PlaySound(pszSound,0,(int) (SND.SND_ASYNC | SND.SND_ALIAS | SND.SND_NOWAIT));
		}
    }
}
