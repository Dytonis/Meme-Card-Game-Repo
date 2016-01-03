namespace Evolutio
{
    public class Chromosome : ScriptableObject
    {
        public bool NewDNA = true;

        public Position CPosition = new Position()
        {
            width = 0.5f,
            length = 0.5f,
            face = 1
        };

        public Size CSize = new Size()
        {
            x = 1,
            y = 1,
            z = 1
        };
        public Appendages CAppendages = new Appendages();

        public sealed class Appendages : IMutatable
        {
            public List<GameObject> Objects = new List<GameObject>();

            public void Mutate(float deviation, params int[] data)
            {
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        GameObject a = GameObject.Instantiate<GameObject>(Overlord.AppendageCloneBase);
                        Objects.Add(a);
                        break;

                    case 1:
                        if (Objects.Count > 0)
                            GameObject.Destroy(Objects[0]);
                        break;
                }
            }
        }

        public sealed class Position : IMutatable
        {
            private float _l;
            private float _w;
            public float length
            {
                get
                {
                    return _l;
                }
                set
                {
                    if (value > 1)
                        _l = 1;
                    else if (value < 0)
                        _l = 0;
                    else _l = value;
                }
            }
            public float width
            {
                get
                {
                    return _w;
                }
                set
                {
                    if (value > 1)
                        _w = 1;
                    else if (value < 0)
                        _w = 0;
                    else _w = value;
                }
            }
            public int face { get; set; }

            public void Mutate(float deviation, params int[] data)
            {
                if (data.Length <= 0)
                {
                    switch (UnityEngine.Random.Range(0, 3))
                    {
                        case 0:
                            length += UnityEngine.Random.Range(-deviation / 2, deviation / 2);
                            break;

                        case 1:
                            width += UnityEngine.Random.Range(-deviation / 2, deviation / 2);
                            break;

                        case 2:
                            face = UnityEngine.Random.Range(1, 7);
                            break;
                    }
                }
                else
                {
                    length += UnityEngine.Random.Range(-deviation / 2, deviation / 2);
                    width += UnityEngine.Random.Range(-deviation / 2, deviation / 2);
                    face = UnityEngine.Random.Range(1, 7);
                }
            }
        }

        public sealed class Size : IMutatable
        {
            public float x { get; set; }
            public float y { get; set; }
            public float z { get; set; }

            public void Mutate(float deviation, params int[] data)
            {
                if (data.Length <= 0)
                {
                    switch (UnityEngine.Random.Range(0, 3))
                    {
                        case 0:
                            x += UnityEngine.Random.Range(-deviation, deviation);
                            break;

                        case 1:
                            y += UnityEngine.Random.Range(-deviation, deviation);
                            break;

                        case 2:
                            z += UnityEngine.Random.Range(-deviation, deviation);
                            break;
                    }
                }
                else
                {
                    x += UnityEngine.Random.Range(-deviation, deviation);
                    y += UnityEngine.Random.Range(-deviation, deviation);
                    z += UnityEngine.Random.Range(-deviation, deviation);
                }
            }
        }

        public void Mutate(bool NewBorn)
        {
            if (!NewBorn)
            {
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        CSize.Mutate(1);
                        break;

                    case 1:
                        CSize.Mutate(1);
                        break;

                    case 2:
                        if (CAppendages.Objects.Count > 0)
                            CPosition.Mutate(1);
                        else
                            CSize.Mutate(1);
                        break;
                }
            }
            else
            {
                CSize.Mutate(1, 1);
                CPosition.Mutate(1, 1);
            }
        }

        public void Copy(Chromosome CopyFrom)
        {
            CSize = CopyFrom.CSize;
            CPosition = CopyFrom.CPosition;
        }
    }

    public interface IMutatable
    {
        void Mutate(float deviation, params int[] data);
    }
}
