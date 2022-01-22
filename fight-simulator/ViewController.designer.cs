// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace fight_simulator
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField BlackLabel { get; set; }

		[Outlet]
		AppKit.NSTextField BlueLabel { get; set; }

		[Outlet]
		AppKit.NSTextField GreenLabel { get; set; }

		[Outlet]
		AppKit.NSTextField RedLabel { get; set; }

		[Outlet]
		SkiaSharp.Views.Mac.SKCanvasView skiaView { get; set; }

		[Action ("AddBoard:")]
		partial void AddBoard (Foundation.NSObject sender);

		[Action ("PopBoard:")]
		partial void PopBoard (Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (skiaView != null) {
				skiaView.Dispose ();
				skiaView = null;
			}

			if (RedLabel != null) {
				RedLabel.Dispose ();
				RedLabel = null;
			}

			if (GreenLabel != null) {
				GreenLabel.Dispose ();
				GreenLabel = null;
			}

			if (BlueLabel != null) {
				BlueLabel.Dispose ();
				BlueLabel = null;
			}

			if (BlackLabel != null) {
				BlackLabel.Dispose ();
				BlackLabel = null;
			}

		}
	}
}
