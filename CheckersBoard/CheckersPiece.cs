// Fig. 13.25: CheckersPiece.cs
// Storage class for checkers piece attributes.

using System;
using System.Drawing;

namespace Checkers
{
   /// <summary>
   /// represents a checkers piece
   /// </summary>
   /// 
	[Serializable]
   public class CheckersPiece
   {
      // define checkers-piece type constants
      public enum Types
      {
         BMAN,
		 BKING,
		 WMAN,
		 WKING,
      }

      private int currentType; // this object's type
      private Bitmap pieceImage; // this object's image
	  private Bitmap m_sourceImage;
      // default display location
      private Rectangle targetRectangle; 
         
	   int m_XIncrement;
	   int m_YIncrement;
	   public int m_spot;
      // construct piece
      public CheckersPiece( int type, int xLocation, 
         int yLocation, int spot,Bitmap sourceImage, int XIncrement,int YIncrement )
      {
		  targetRectangle = new Rectangle( 0, 0, XIncrement, YIncrement );
         currentType = type; // set current type
         targetRectangle.X = xLocation; // set current x location
         targetRectangle.Y = yLocation; // set current y location
			m_XIncrement = XIncrement;
		  m_YIncrement = YIncrement;
         // obtain pieceImage from section of sourceImage
         pieceImage = sourceImage.Clone( 
            new Rectangle( type * m_XIncrement, 0, m_XIncrement, m_YIncrement ), 
            System.Drawing.Imaging.PixelFormat.DontCare );
		  m_spot = spot;
		  m_sourceImage = sourceImage;
      }

      // draw checkers piece
      public void Draw( Graphics graphicsObject )
      {
         graphicsObject.DrawImage( pieceImage, targetRectangle );
      } 
	public void PrevDraw( Graphics graphicsObject, int x, int y )
	{
		Rectangle rect = targetRectangle;
		rect.Offset(x,y);
		graphicsObject.DrawImage( pieceImage, rect );
	} 

      // obtain this piece's location rectangle
      public Rectangle GetBounds()
      {
         return targetRectangle;
      } // end method GetBounds

      // set this piece's location
      public void SetLocation( int xLocation, int yLocation, bool state)
      {
         targetRectangle.X = xLocation;
         targetRectangle.Y = yLocation;
		  if(currentType > (int)Types.BMAN && state)
			  targetRectangle.Y += 4;

      }

   	public void MorpheTo(int type)
	   {
		 currentType = type;
		 // obtain pieceImage from section of sourceImage
         pieceImage = m_sourceImage.Clone( 
            new Rectangle( type * m_XIncrement, 0, m_XIncrement, m_YIncrement ), 
            System.Drawing.Imaging.PixelFormat.DontCare );
	   }

   	public int GetCurrentType()
	   {
	   	return currentType;
	   } // end method SetLocation

   } // end class CheckersPiece
}



