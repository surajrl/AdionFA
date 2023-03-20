namespace Tee
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootNode = new StartNodeAssembledModel { Label = "Start"};
            rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "1" }, 0);
            rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "2" }, 0);
            rootNode.TransitionTo(new EndNodeAssembledModel { Label = "End" }, 1);
            rootNode.TransitionTo(new EndNodeAssembledModel { Label = "End" }, 1,20);

            //rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "1" }, 1);
            //rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "2" }, 1);
            //rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "3" }, 1);
            //rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "4" }, 1);
            //
            //rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "1" }, 2);
            //rootNode.TransitionTo(new BacktestNodeAssembledModel { Label = "2" }, 2);
            //
            //rootNode.InsertTo(new TreeNode { Label = "Insert " }, 2);

            var observableNode = rootNode.MapToTreeObservableNode();
        }
    }
}
