using DomainObjects.Blocks.Details;

namespace SmartInfo.Helpers;

public class ScenarioScheduler
{
    public List<Scene> Scenes { get; set; }

    public Scene GetNextScene(DateTime dateTime, int currentFrameIndex)
    {
        var dayOfWeek = dateTime.DayOfWeek;
        var frames = Scenes
            .Where(HaveDuration())
            .Where(IsInTimeInterval(dateTime))
            .Where(IsInDate(dateTime))
            .Where(IsInDayOfWeek(dayOfWeek));
        return frames.FirstOrDefault(f => f.Index > currentFrameIndex) ?? frames.FirstOrDefault();
    }

    private static Func<Scene, bool> IsInDayOfWeek(DayOfWeek dayOfWeek) =>
        f => !f.UseInDayOfWeek 
             || (f.UseInMon && dayOfWeek.Equals(DayOfWeek.Monday))
             || (f.UseInTue && dayOfWeek.Equals(DayOfWeek.Tuesday))
             || (f.UseInWed && dayOfWeek.Equals(DayOfWeek.Wednesday))
             || (f.UseInThu && dayOfWeek.Equals(DayOfWeek.Thursday))
             || (f.UseInFri && dayOfWeek.Equals(DayOfWeek.Friday))
             || (f.UseInSat && dayOfWeek.Equals(DayOfWeek.Saturday))
             || (f.UseInSun && dayOfWeek.Equals(DayOfWeek.Sunday));

    private static Func<Scene, bool> IsInDate(DateTime dateTime) =>
        f => !f.UseInDate 
             || (f.DateToUse.HasValue && f.DateToUse.Value.Date.Equals(dateTime.Date));

    private static Func<Scene, bool> HaveDuration() => 
        f => f.Duration > 0;

    private static Func<Scene, bool> IsInTimeInterval(DateTime dateTime) =>
        f => !f.UseInTimeInterval 
             || (f is {UseFromTime: { }, UseToTime: { }} && f.UseFromTime.Value <= dateTime.TimeOfDay);

}