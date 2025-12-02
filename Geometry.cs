namespace GeometryTask;

public class Vector
{
    public double X;
    public double Y;

    public double GetLength()
    {
        return Geometry.GetLength(this);
    }

    public Vector Add(Vector v)
    {
        return Geometry.Add(this,v);
    }

    public bool Belongs(Segment segment)
    {
        return Geometry.IsVectorInSegment(this, segment);
    }
}

public class Geometry
{
    public static double GetLength(Vector vector)
    {
        return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
    }

    public static Vector Add(Vector vector1, Vector vector2)
    {
        return new Vector 
        { 
            X = vector1.X + vector2.X,
            Y = vector1.Y + vector2.Y,
        };
    }

    public static double GetLength(Segment segment)
    {
        var segmentX = segment.End.X - segment.Begin.X;
        var segmentY = segment.End.Y - segment.Begin.Y;
        return Math.Sqrt(segmentX * segmentX + segmentY * segmentY);
    }

    public static bool IsVectorInSegment(Vector vector, Segment segment)
    {
        var epsilon = 1e-10;

        var segmX = segment.End.X - segment.Begin.X;
        var segmY = segment.End.Y - segment.Begin.Y;

        var beginToPointX = vector.X - segment.Begin.X;
        var beginToPointY = vector.Y - segment.Begin.Y;

        var collinear = segmX * beginToPointY - segmY * beginToPointX;

        if (Math.Abs(collinear) > epsilon) 
            return false;

        var point = segmX * beginToPointX + segmY * beginToPointY;
        var lengthSegm = Math.Sqrt(segmX * segmX + segmY * segmY);

        if (lengthSegm < epsilon)
            return Math.Abs(beginToPointX) < epsilon && Math.Abs(beginToPointY) < epsilon;

        var projection = point / (lengthSegm * lengthSegm);
        return projection >= 0.0 && projection <= 1.0;
    }
}

public class Segment
{
    public Vector Begin;
    public Vector End;

    public double GetLength()
    {
        return Geometry.GetLength(this);
    }

    public bool Contains(Vector vector)
    {
        return Geometry.IsVectorInSegment(vector, this);
    } 
}
