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

		}
	}
}
