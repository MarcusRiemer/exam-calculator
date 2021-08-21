﻿using Avalonia;
using Avalonia.Logging;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;

namespace ExamCalculator.UI
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            Locator.CurrentMutable.Register(() => new PupilOverview(), typeof(IViewFor<PupilOverviewViewModel>));
            Locator.CurrentMutable.Register(() => new ExamOverview(), typeof(IViewFor<ExamOverviewViewModel>));
            Locator.CurrentMutable.Register(() => new GroupOverview(), typeof(IViewFor<GroupOverviewViewModel>));

            return AppBuilder.Configure<App>()
                .UseReactiveUI()
                .UsePlatformDetect()
                .LogToTrace(LogEventLevel.Debug);
        }
    }
}