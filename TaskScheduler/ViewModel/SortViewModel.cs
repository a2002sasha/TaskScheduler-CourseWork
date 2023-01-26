using ToDoList.Enums;

namespace ToDoList.ViewModel
{
    public class SortViewModel
    {
        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            DescriptionSort = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            TaskLevelSort = sortOrder == SortState.TaskLevelAsc ? SortState.TaskLevelDesc : SortState.TaskLevelAsc;
            TaskStateSort = sortOrder == SortState.TaskStateAsc ? SortState.TaskStateDesc : SortState.TaskStateAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            TimeSort = sortOrder == SortState.TimeAsc ? SortState.TimeDesc : SortState.TimeAsc;
        }

        public SortState NameSort { get; private set; }
        public SortState DescriptionSort { get; private set; }
        public SortState TaskLevelSort { get; private set; }
        public SortState TaskStateSort { get; private set; }
        public SortState DateSort { get; private set; } 
        public SortState TimeSort { get; private set; }
    }
}
