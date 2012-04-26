using System.Collections.Generic;
using System.Text;

namespace FubuCollectionBindingProblem.Handlers
{
    public class SayHelloController
    {
        public SayHelloModel Query()
        {
            return new SayHelloModel
            {
                Room = new Room
                {
                    Peoples = new List<People>
                    {
                        new People {Name = "James"},
                        new People {Name = "Jhon"},
                        new People {Name = "Timmy"}
                    }
                },
                Pattern = "Hello {0}"
            };
        }

        public SayHelloResponse Command(SayHelloModel input)
        {
            var builder = new StringBuilder();
            // FIXME: input.Room.Peoples is empty!
            input.Room.Peoples.Each(x => builder.AppendLine(string.Format(input.Pattern, x.Name)));
            return new SayHelloResponse
            {
                Message = builder.ToString(),
            };
        }


    }

    public class SayHelloModel
    {
        public SayHelloModel()
        {
            Room = new Room();
        }
        public string Pattern { get; set; }
        public Room Room { get; set; }
    }


    public class SayHelloResponse
    {
        public string Message { get; set; }
    }

    public class Room
    {
        public Room()
        {
            Peoples = new List<People>();
        }
        public IList<People> Peoples { get; set; }
    }

    public class People
    {
        public string Name { get; set; }
    }


}