using Helpers;

namespace ArrayListDictionaryRaceFiles
{
    public class TestLinkedList
    {
        public static async Task WriteToLinkedList(int numObjects, bool WriteString, bool WriteInt, bool WriteRandomValues)
        {
            if (WriteInt)
            {
                await WriteIntLinkedListAddMiddle(numObjects, WriteRandomValues);
            }
            if (WriteString)
            {
                await WriteStringLinkedListAddMiddle(numObjects, WriteRandomValues);
            }
        }

        public static async Task WriteThenOverWriteLinkedList(int objectsToWrite, bool WriteInt, bool WriteString)
        {
            if (WriteInt)
            {
                await WriteThenOverWriteIntLinkedList(objectsToWrite);

            }
            if (WriteString)
            {
                await WriteThenOverWriteStringLinkedList(objectsToWrite);
            }
        }
        
        public static async Task WriteStringLinkedListAddMiddle(int numObjects, bool WriteRandomValues)
        {
            await Task.Run(() =>
            {
                var linkedListString = new LinkedList<string>();
                linkedListString.AddFirst(HelperMethods.GetAlphaNumericString(5) + "-0");
                
                if (WriteRandomValues)
                {
                    for (int i = 1; i < numObjects; i++)
                    {
                        int steps = linkedListString.Count / 2;
                        var node = linkedListString.First;

                        for (int s = 0; s < steps; s++)
                        {
                            node = node.Next;
                        }
                        
                        linkedListString.AddBefore(node, HelperMethods.GetAlphaNumericString(5) + "-" + i);
                    }
                }
                else
                {
                    for (int i = 1; i < numObjects; i++)
                    {
                        int steps = linkedListString.Count / 2;
                        var node = linkedListString.First;

                        for (int s = 0; s < steps; s++)
                        {
                            node = node.Next;
                        }
                        
                        linkedListString.AddBefore(node, i.ToString());
                    }
                }

            });
        }
        
        
        public static async Task WriteIntLinkedListAddMiddle(int numObjects, bool WriteRandomValues)
        {
            await Task.Run(() =>
            {
                
                var linkedListInt = new LinkedList<int>();
                linkedListInt.AddFirst(0);
                
                if (WriteRandomValues)
                {
                    for (int i = 1; i < numObjects; i++)
                    {
                        int steps = linkedListInt.Count / 2;
                        var node = linkedListInt.First;

                        for (int s = 0; s < steps; s++)
                        {
                            node = node.Next;
                        }
                        
                        linkedListInt.AddBefore(node, new Random().Next(1000));
                    }
                }
                else
                {
                    for (int i = 1; i < numObjects; i++)
                    {
                        int steps = linkedListInt.Count / 2;
                        var node = linkedListInt.First;

                        for (int s = 0; s < steps; s++)
                        {
                            node = node.Next;
                        }
                        
                        linkedListInt.AddBefore(node, i);
                    }
                }

            });            
        }
        
        private static async Task WriteThenOverWriteStringLinkedList(int objectsToWrite)
        {

            await Task.Run(() =>
            {
                var linkedListString = new LinkedList<string>();
                linkedListString.AddFirst(HelperMethods.GetAlphaNumericString(5) + "-0");
                
                for (int i = 1; i < objectsToWrite; i++)
                {
                    int steps = linkedListString.Count / 2;
                    var node = linkedListString.First;

                    for (int s = 0; s < steps; s++)
                    {
                        node = node.Next;
                    }
                        
                    linkedListString.AddBefore(node, HelperMethods.GetAlphaNumericString(5) + "-" + i);
                }
                
                // overwrite all values, starting from position 0
                var currentNode = linkedListString.First;

                while (currentNode != null)
                {
                    currentNode.Value = HelperMethods.GetAlphaNumericString(5);
                    currentNode = currentNode.Next;
                }
                
            });
        }

        private static async Task WriteThenOverWriteIntLinkedList(int objectsToWrite)
        {
            await Task.Run(() =>
            {
                var linkedListInt = new LinkedList<int>();
                linkedListInt.AddFirst(0);
                
                for (int i = 1; i < objectsToWrite; i++)
                {
                    int steps = linkedListInt.Count / 2;
                    var node = linkedListInt.First;

                    for (int s = 0; s < steps; s++)
                    {
                        node = node.Next;
                    }
                    
                    linkedListInt.AddBefore(node, i);
                }

                // overwrite all values, starting from position 0
                var currentNode = linkedListInt.First;
                int idx = 0;

                while (currentNode != null)
                {
                    currentNode.Value = idx++;
                    currentNode = currentNode.Next;
                }
            });
        }
    }    
}
