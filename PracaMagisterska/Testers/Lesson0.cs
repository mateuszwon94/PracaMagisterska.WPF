﻿namespace PracaMagisterska.WPF.Testers {
    /// <summary>
    /// Abstract class representing lesson zero
    /// </summary>
    public sealed class Lesson0 : Lesson {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Lesson0() : base(0, "Witaj Świecie", 0f, 0) 
            => InitializeTest();

        /// <summary>
        /// Constructor.
        /// </summary>
        public Lesson0(bool[] results) : base(0, "Witaj Świecie", 0f, results) 
            => InitializeTest();

        /// <inheritdoc cref="Lesson.DefaultCode"/>
        public override string DefaultCode { get; } = defaultProgramWithMain_;
        
        /// <inheritdoc cref="Lesson.Info"/>
        public override string Info { get; } =
@"Oto Twój pierwszy program. Przejdźmy linijka po linijce i wytłumaczę Ci, co się w nim dzieje.
W pierwszej linijce przy pomocy słowa kluczowego using importowana jest podstawowa biblioteka. W niej znajdują się wszystkie najprostsze i najpotrzebniejsze typy.
Ponieważ C# jest czysto obiektowym językiem, to w linijce trzeciej zdefiniowana jest klasa (czym to dokładnie jest, dowiesz się później). Ma ona w sobie metodę Main. Tak nazywa się metodę, którą system operacyjny uruchamia jako pierwszą. W jej ciele z obiektu Console wywoływana jest metoda WriteLine, która wypisuje na ekran tekst z cudzysłowu.
Spróbuj skompilować i uruchomić ten program. Co zobaczyłeś w konsoli? Teraz spróbuj zmienić tekst i uruchomić program jeszcze raz.";
    }
}
