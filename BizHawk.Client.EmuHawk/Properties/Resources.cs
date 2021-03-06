using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BizHawk.Client.EmuHawk.Properties {
	internal class Resources {
		private static readonly Assembly Asm = Assembly.GetExecutingAssembly();

		/// <param name="embedPath">Dir separator is '<c>.</c>'. Path is relative to <c>&lt;NS></c>.</param>
		private static Bitmap ReadEmbeddedBitmapAt(string embedPath) => new Bitmap(Asm.GetManifestResourceStream($"BizHawk.Client.EmuHawk.{embedPath}"));

		/// <param name="filename">Dir separator is '<c>.</c>'. Filename is relative to <c>&lt;NS>/images</c> and omits <c>.png</c> extension.</param>
		/// <remarks>For other file extensions or paths use <see cref="ReadEmbeddedBitmapAt"/>.</remarks>
		private static Bitmap ReadEmbeddedBitmap(string filename) => ReadEmbeddedBitmapAt($"images.{filename}.png");

		/// <param name="filename">Dir separator is '<c>.</c>'. Filename is relative to <c>&lt;NS>/images</c> and omits <c>.ico</c> extension.</param>
		private static Icon ReadEmbeddedIcon(string filename) => new Icon(Asm.GetManifestResourceStream($"BizHawk.Client.EmuHawk.images.{filename}.ico"));

		internal static readonly Bitmap add = ReadEmbeddedBitmap("add");
		internal static readonly Bitmap AddEdit = ReadEmbeddedBitmap("AddEdit");
		internal static readonly Bitmap addWatch = ReadEmbeddedBitmapAt("images.addWatch.ico");
		internal static readonly Bitmap arrow_black_down = ReadEmbeddedBitmap("arrow_black_down");
		internal static readonly Lazy<Bitmap> atari_controller = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("atari_controller"));
		internal static readonly Bitmap AudioHS = ReadEmbeddedBitmap("AudioHS");
		internal static readonly Bitmap AutoSearch = ReadEmbeddedBitmap("AutoSearch");
		internal static readonly Bitmap AVI = ReadEmbeddedBitmap("AVI");
		internal static readonly Bitmap Back = ReadEmbeddedBitmap("Back");
		internal static readonly Bitmap BackMore = ReadEmbeddedBitmap("BackMore");
		internal static readonly Icon basicbot = ReadEmbeddedIcon("basicbot");
		internal static readonly Bitmap Blank = ReadEmbeddedBitmap("Blank");
		internal static readonly Cursor BlankCursor = new Cursor(Asm.GetManifestResourceStream("BizHawk.Client.EmuHawk.images.BlankCursor.cur"));
		internal static readonly Bitmap BlueDown = ReadEmbeddedBitmap("BlueDown");
		internal static readonly Bitmap BlueUp = ReadEmbeddedBitmap("BlueUp");
		internal static readonly Bitmap Both = ReadEmbeddedBitmap("Both");
		internal static readonly Bitmap bsnes = ReadEmbeddedBitmap("bsnes");
		internal static readonly Icon Bug_MultiSize = ReadEmbeddedIcon("Bug");
		internal static readonly Bitmap Bug = ReadEmbeddedBitmap("Bug");
		internal static readonly Icon calculator_MultiSize = ReadEmbeddedIcon("calculator");
		internal static readonly Bitmap calculator = ReadEmbeddedBitmap("calculator");
		internal static readonly Bitmap camera = ReadEmbeddedBitmap("camera");
		internal static readonly Bitmap cdlogger = ReadEmbeddedBitmapAt("images.cdlogger.ico");
		internal static readonly Icon cdlogger_MultiSize = ReadEmbeddedIcon("cdlogger");
		internal static readonly Bitmap checkbox = ReadEmbeddedBitmap("checkbox");
		internal static readonly Bitmap Circle = ReadEmbeddedBitmap("Circle");
		internal static readonly Bitmap Close = ReadEmbeddedBitmap("Close");
		internal static readonly Bitmap connect_16x16 = ReadEmbeddedBitmap("connect_16x16");
		internal static readonly Lazy<Bitmap> A78Joystick = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.A78Joystick"));
		internal static readonly Lazy<Bitmap> AppleIIKeyboard = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.AppleIIKeyboard"));
		internal static readonly Lazy<Bitmap> ArcadeController = new Lazy<Bitmap>(() => ReadEmbeddedBitmapAt("images.ControllerImages.ArcadeController.jpg"));
		internal static readonly Lazy<Bitmap> C64Joystick = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.C64Joystick"));
		internal static readonly Lazy<Bitmap> C64Keyboard = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.C64Keyboard"));
		internal static readonly Lazy<Bitmap> colecovisioncontroller = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.colecovisioncontroller"));
		internal static readonly Lazy<Bitmap> GBA_Controller = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.GBA_Controller"));
		internal static readonly Lazy<Bitmap> GBController = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.GBController"));
		internal static readonly Lazy<Bitmap> GENController = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.GENController"));
		internal static readonly Lazy<Bitmap> IntVController = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.IntVController"));
		internal static readonly Lazy<Bitmap> Lynx = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.Lynx"));
		internal static readonly Lazy<Bitmap> N64 = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.N64"));
		internal static readonly Lazy<Bitmap> NES_Controller = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.NES_Controller"));
		internal static readonly Lazy<Bitmap> NGPController = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.NGPController"));
		internal static readonly Lazy<Bitmap> PCEngineController = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.PCEngineController"));
		internal static readonly Lazy<Bitmap> psx_dualshock = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.psx_dualshock"));
		internal static readonly Lazy<Bitmap> SaturnController = new Lazy<Bitmap>(() => ReadEmbeddedBitmapAt("images.ControllerImages.SaturnController.jpg"));
		internal static readonly Lazy<Bitmap> SMSController = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.SMSController"));
		internal static readonly Lazy<Bitmap> SNES_Controller = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.SNES_Controller"));
		internal static readonly Lazy<Bitmap> TI83_Controller = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.TI83_Controller"));
		internal static readonly Lazy<Bitmap> VBoyController = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.VBoyController"));
		internal static readonly Lazy<Bitmap> WonderSwanColor = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.WonderSwanColor"));
		internal static readonly Lazy<Bitmap> ZXSpectrumKeyboards = new Lazy<Bitmap>(() => ReadEmbeddedBitmap("ControllerImages.ZXSpectrumKeyboards"));
		internal static readonly Bitmap CopyFolderHS = ReadEmbeddedBitmap("CopyFolderHS");
		internal static readonly Bitmap corphawk = ReadEmbeddedBitmapAt("images.corphawk.jpg");
		internal static readonly Bitmap CorpHawkSmall = ReadEmbeddedBitmap("CorpHawkSmall");
		internal static readonly Bitmap Cross = ReadEmbeddedBitmap("Cross");
		internal static readonly Bitmap CutHS = ReadEmbeddedBitmap("CutHS");
		internal static readonly Bitmap Debugger = ReadEmbeddedBitmap("Debugger");
		internal static readonly Bitmap Delete = ReadEmbeddedBitmap("Delete");
		internal static readonly Icon dual_MultiSize = ReadEmbeddedIcon("dual");
		internal static readonly Bitmap Duplicate = ReadEmbeddedBitmap("Duplicate");
		internal static readonly Bitmap ENE = ReadEmbeddedBitmap("ENE");
		internal static readonly Bitmap Erase = ReadEmbeddedBitmap("Erase");
		internal static readonly Bitmap ESE = ReadEmbeddedBitmap("ESE");
		internal static readonly Bitmap ExclamationRed = ReadEmbeddedBitmap("ExclamationRed");
		internal static readonly Bitmap FastForward = ReadEmbeddedBitmap("FastForward");
		internal static readonly Bitmap FindHS = ReadEmbeddedBitmap("FindHS");
		internal static readonly Bitmap Forward = ReadEmbeddedBitmap("Forward");
		internal static readonly Icon Freeze_MultiSize = ReadEmbeddedIcon("Freeze");
		internal static readonly Bitmap Freeze = ReadEmbeddedBitmap("Freeze");
		internal static readonly Bitmap Fullscreen = ReadEmbeddedBitmap("Fullscreen");
		internal static readonly Icon gambatte_MultiSize = ReadEmbeddedIcon("gambatte");
		internal static readonly Bitmap gambatte = ReadEmbeddedBitmap("gambatte");
		internal static readonly Icon GameController_MultiSize = ReadEmbeddedIcon("GameController");
		internal static readonly Bitmap GameController = ReadEmbeddedBitmap("GameController");
		internal static readonly Lazy<Icon> gba_MultiSize = new Lazy<Icon>(() => ReadEmbeddedIcon("Gameboy Advance (black) icon"));
		internal static readonly Bitmap genplus = ReadEmbeddedBitmap("genplus");
		internal static readonly Bitmap GreenCheck = ReadEmbeddedBitmap("GreenCheck");
		internal static readonly Bitmap Hack = ReadEmbeddedBitmap("Hack");
		internal static readonly Bitmap Help = ReadEmbeddedBitmap("Help");
		internal static readonly Bitmap HomeBrew = ReadEmbeddedBitmap("HomeBrew");
		internal static readonly Icon HotKeys_MultiSize = ReadEmbeddedIcon("HotKeys");
		internal static readonly Bitmap HotKeys = ReadEmbeddedBitmap("HotKeys");
		internal static readonly Bitmap Import = ReadEmbeddedBitmap("Import");
		internal static readonly Bitmap InsertSeparator = ReadEmbeddedBitmap("InsertSeparator");
		internal static readonly Bitmap JumpTo = ReadEmbeddedBitmap("JumpTo");
		internal static readonly Bitmap kitchensink = ReadEmbeddedBitmap("kitchensink");
		internal static readonly Icon Lightning_MultiSize = ReadEmbeddedIcon("Lightning");
		internal static readonly Bitmap Lightning = ReadEmbeddedBitmap("Lightning");
		internal static readonly Bitmap LightOff = ReadEmbeddedBitmap("LightOff");
		internal static readonly Bitmap LightOn = ReadEmbeddedBitmap("LightOn");
		internal static readonly Bitmap LoadConfig = ReadEmbeddedBitmap("LoadConfig");
		internal static readonly Icon logo = ReadEmbeddedIcon("logo");
		internal static readonly Bitmap Lua = ReadEmbeddedBitmap("Lua");
		internal static readonly Bitmap luaPictureBox = ReadEmbeddedBitmap("luaPictureBox");
		internal static readonly Bitmap mame = ReadEmbeddedBitmap("mame");
		internal static readonly Bitmap MessageConfig = ReadEmbeddedBitmap("MessageConfig");
		internal static readonly Bitmap mGba = ReadEmbeddedBitmap("mgba-16");
		internal static readonly Icon monitor_MultiSize = ReadEmbeddedIcon("monitor");
		internal static readonly Bitmap monitor = ReadEmbeddedBitmap("monitor");
		internal static readonly Bitmap MoveBottom = ReadEmbeddedBitmapAt("Resources.MoveBottom.png");
		internal static readonly Bitmap MoveDown = ReadEmbeddedBitmap("MoveDown");
		internal static readonly Bitmap MoveLeft = ReadEmbeddedBitmap("MoveLeft");
		internal static readonly Bitmap MoveRight = ReadEmbeddedBitmap("MoveRight");
		internal static readonly Bitmap MoveTop = ReadEmbeddedBitmapAt("Resources.MoveTop.png");
		internal static readonly Bitmap MoveUp = ReadEmbeddedBitmap("MoveUp");
		internal static readonly Icon MsgBox_MultiSize = ReadEmbeddedIcon("MsgBox");
		internal static readonly Bitmap NE = ReadEmbeddedBitmap("NE");
		internal static readonly Icon NESControllerIcon_MultiSize = ReadEmbeddedIcon("NESControllerIcon");
		internal static readonly Bitmap NewFile = ReadEmbeddedBitmap("NewFile");
		internal static readonly Bitmap NNE = ReadEmbeddedBitmap("NNE");
		internal static readonly Bitmap NNW = ReadEmbeddedBitmap("NNW");
		internal static readonly Bitmap noconnect_16x16 = ReadEmbeddedBitmap("noconnect_16x16");
		internal static readonly Stream nothawk = Asm.GetManifestResourceStream("BizHawk.Client.EmuHawk.Resources.nothawk.bin");
		internal static readonly Bitmap NW = ReadEmbeddedBitmap("NW");
		internal static readonly Bitmap OpenFile = ReadEmbeddedBitmap("OpenFile");
		internal static readonly Bitmap Paste = ReadEmbeddedBitmap("Paste");
		internal static readonly Bitmap Pause = ReadEmbeddedBitmap("Pause");
		internal static readonly Bitmap pcb = ReadEmbeddedBitmap("pcb");
		internal static readonly Icon pce_MultiSize = ReadEmbeddedIcon("pce");
		internal static readonly Icon pencil_MultiSize = ReadEmbeddedIcon("pencil");
		internal static readonly Bitmap pencil = ReadEmbeddedBitmap("pencil");
		internal static readonly Bitmap Play = ReadEmbeddedBitmap("Play");
		internal static readonly Bitmap placeholder_bitmap = ReadEmbeddedBitmap("placeholder_bitmap");
		internal static readonly Icon poke_MultiSize = ReadEmbeddedIcon("poke");
		internal static readonly Bitmap poke = ReadEmbeddedBitmap("poke");
		internal static readonly Bitmap ppsspp = ReadEmbeddedBitmap("ppsspp");
		internal static readonly Icon Previous_MultiSize = ReadEmbeddedIcon("Previous");
		internal static readonly Bitmap Previous = ReadEmbeddedBitmap("Previous");
		internal static readonly Icon QuickNes_MultiSize = ReadEmbeddedIcon("QuickNes");
		internal static readonly Bitmap QuickNes = ReadEmbeddedBitmap("QuickNes");
		internal static readonly Bitmap ReadOnly = ReadEmbeddedBitmap("ReadOnly");
		internal static readonly Bitmap reboot = ReadEmbeddedBitmap("reboot");
		internal static readonly Bitmap Recent = ReadEmbeddedBitmap("Recent");
		internal static readonly Bitmap RecordHS = ReadEmbeddedBitmap("RecordHS");
		internal static readonly Bitmap redo = ReadEmbeddedBitmap("redo");
		internal static readonly Bitmap Refresh = ReadEmbeddedBitmapAt("images.Refresh.bmp");
		internal static readonly Bitmap Refresh1 = ReadEmbeddedBitmap("Refresh");
		internal static readonly Bitmap restart = ReadEmbeddedBitmap("restart");
		internal static readonly Bitmap RetroQuestion = ReadEmbeddedBitmap("RetroQuestion");
		internal static readonly Bitmap Save = ReadEmbeddedBitmap("Save");
		internal static readonly Bitmap SaveAs = ReadEmbeddedBitmap("SaveAs");
		internal static readonly Bitmap SaveConfig = ReadEmbeddedBitmap("SaveConfig");
		internal static readonly Bitmap Scan = ReadEmbeddedBitmap("Scan");
		internal static readonly Bitmap ScrollTo = ReadEmbeddedBitmap("ScrollTo");
		internal static readonly Bitmap SE = ReadEmbeddedBitmap("SE");
		internal static readonly Icon search_MultiSize = ReadEmbeddedIcon("search");
		internal static readonly Bitmap search = ReadEmbeddedBitmap("search");
		internal static readonly Icon Shark_MultiSize = ReadEmbeddedIcon("Shark");
		internal static readonly Bitmap Shark = ReadEmbeddedBitmap("Shark");
		internal static readonly Icon sms_MultiSize = ReadEmbeddedIcon("sms-icon");
		internal static readonly Bitmap snes9x = ReadEmbeddedBitmap("snes9x");
		internal static readonly Bitmap Square = ReadEmbeddedBitmap("Square");
		internal static readonly Bitmap SSE = ReadEmbeddedBitmap("SSE");
		internal static readonly Bitmap SSW = ReadEmbeddedBitmap("SSW");
		internal static readonly Bitmap Stop = ReadEmbeddedBitmap("Stop");
		internal static readonly Bitmap StopButton = ReadEmbeddedBitmap("StopButton");
		internal static readonly Bitmap SW = ReadEmbeddedBitmap("SW");
		internal static readonly Icon TAStudio_MultiSize = ReadEmbeddedIcon("TAStudio");
		internal static readonly Bitmap TAStudio = ReadEmbeddedBitmap("TAStudio");
		internal static readonly Bitmap icon_anchor = ReadEmbeddedBitmap("tastudio.icon_anchor");
		internal static readonly Bitmap icon_anchor_lag = ReadEmbeddedBitmap("tastudio.icon_anchor_lag");
		internal static readonly Bitmap icon_marker = ReadEmbeddedBitmap("tastudio.icon_marker");
		internal static readonly Bitmap ts_h_arrow_blue = ReadEmbeddedBitmap("tastudio.ts_h_arrow_blue");
		internal static readonly Bitmap ts_h_arrow_green = ReadEmbeddedBitmap("tastudio.ts_h_arrow_green");
		internal static readonly Bitmap ts_h_arrow_green_blue = ReadEmbeddedBitmap("tastudio.ts_h_arrow_green_blue");
		internal static readonly Bitmap ts_v_arrow_blue = ReadEmbeddedBitmap("tastudio.ts_v_arrow_blue");
		internal static readonly Bitmap ts_v_arrow_green = ReadEmbeddedBitmap("tastudio.ts_v_arrow_green");
		internal static readonly Bitmap ts_v_arrow_green_blue = ReadEmbeddedBitmap("tastudio.ts_v_arrow_green_blue");
		internal static readonly Icon textdoc_MultiSize = ReadEmbeddedIcon("textdoc");
		internal static readonly Bitmap thumbsdown = ReadEmbeddedBitmap("thumbsdown");
		internal static readonly Icon ToolBox_MultiSize = ReadEmbeddedIcon("ToolBox");
		internal static readonly Bitmap ToolBox = ReadEmbeddedBitmap("ToolBox");
		internal static readonly Bitmap Translation = ReadEmbeddedBitmap("Translation");
		internal static readonly Bitmap Triangle = ReadEmbeddedBitmap("Triangle");
		internal static readonly Bitmap TruncateFromFile = ReadEmbeddedBitmap("TruncateFromFile");
		internal static readonly Bitmap tvIcon = ReadEmbeddedBitmap("tvIcon");
		internal static readonly Bitmap undo = ReadEmbeddedBitmap("undo");
		internal static readonly Bitmap Unfreeze = ReadEmbeddedBitmap("Unfreeze");
		internal static readonly Icon user_blue = ReadEmbeddedIcon("user_blue");
		internal static readonly Bitmap user_blue_small = ReadEmbeddedBitmap("user_blue_small");
		internal static readonly Icon watch_MultiSize = ReadEmbeddedIcon("watch");
		internal static readonly Bitmap watch = ReadEmbeddedBitmapAt("images.watch.ico");
		internal static readonly Bitmap whiteTriDown = ReadEmbeddedBitmap("whiteTriDown");
		internal static readonly Bitmap whiteTriLeft = ReadEmbeddedBitmap("whiteTriLeft");
		internal static readonly Bitmap whiteTriRight = ReadEmbeddedBitmap("whiteTriRight");
		internal static readonly Bitmap whiteTriUp = ReadEmbeddedBitmap("whiteTriUp");
		internal static readonly Bitmap WNW = ReadEmbeddedBitmap("WNW");
		internal static readonly Bitmap WSW = ReadEmbeddedBitmap("WSW");
		internal static readonly Bitmap YellowDown = ReadEmbeddedBitmap("YellowDown");
		internal static readonly Bitmap YellowLeft = ReadEmbeddedBitmap("YellowLeft");
		internal static readonly Bitmap YellowRight = ReadEmbeddedBitmap("YellowRight");
		internal static readonly Bitmap YellowUp = ReadEmbeddedBitmap("YellowUp");
	}
}
