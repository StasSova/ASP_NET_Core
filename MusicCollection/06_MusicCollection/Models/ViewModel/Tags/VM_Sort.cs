namespace _06_MusicCollection.Models.ViewModel.Tags
{
    public enum SortState
    {
        TitleAsc,
        TitleDesc,
        DurationAsc,
        DurationDesc,
        AlbumAsc,
        AlbumDesc,
    }
    public class VM_Sort
    {
        public SortState Title { get; set; }
        public SortState Album { get; set; }
        public SortState Duration { get; set; }
        public SortState Current { get; set; }

        public VM_Sort(SortState sortOrder)
        {
            // значения по умолчанию
            Title = SortState.TitleAsc;
            Album = SortState.AlbumAsc;
            Duration = SortState.DurationAsc;

            Current = sortOrder;

            Title = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            Album = sortOrder == SortState.AlbumAsc ? SortState.AlbumDesc : SortState.AlbumAsc;
            Duration = sortOrder == SortState.DurationAsc ? SortState.DurationDesc : SortState.DurationAsc;
        }

    }
}
