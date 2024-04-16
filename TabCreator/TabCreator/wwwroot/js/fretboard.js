(function () {
    const root = document.documentElement;
    const fretboard = document.querySelector('.fretboard');
    const accidentalSelector = document.querySelector('.accidental-selector');
    const singleFretMarkPositions = [3, 5, 7, 9, 15, 17, 19, 21];
    const doubleFretMarkPositions = [12, 24];
    const notesFlat = ["C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B"];
    const notesSharp = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];
    const openNotesOctave = [4, 3, 3, 3, 2, 2];
    const guitarStandardTuning = [4, 11, 7, 2, 9, 4];

    let allNotes;
    let accidentals = 'flats';
    let numberOfFrets = 25;
    let numberOfStrings = 6;
    let currentOctave = 2;

    const app = {
        init() {
            this.setupFretboard();
            handlers.setupEventListeners();
        },
        setupFretboard() {
            fretboard.innerHTML = '';
            root.style.setProperty('--number-of-strings', numberOfStrings);
            // Add strings to fretboard
            for (let i = 0; i < numberOfStrings; i++) {
                currentOctave = openNotesOctave[i];

                let string = tools.createElement('div');
                string.classList.add('string');
                fretboard.appendChild(string);

                // Create frets
                for (let fret = 0; fret <= numberOfFrets; fret++) {
                    let noteFret = tools.createElement('div');
                    noteFret.classList.add('note-fret');
                    string.appendChild(noteFret);

                    let noteName = this.generateNoteNames((fret + guitarStandardTuning[i]), accidentals, currentOctave);

                    if (noteName.includes("B") && !noteName.includes("b")) {
                        currentOctave += 1;
                    }

                    noteFret.setAttribute('data-note', noteName);

                    // Add single fret marks
                    if (i === 0 && singleFretMarkPositions.indexOf(fret) !== -1) {
                        noteFret.classList.add('single-fretmark');
                    }

                    if (i === 0 && doubleFretMarkPositions.indexOf(fret) !== -1) {
                        let doubleFretMark = tools.createElement('div');
                        doubleFretMark.classList.add('double-fretmark');
                        noteFret.appendChild(doubleFretMark);
                    }

                }
            }
            allNotes = document.querySelectorAll('.note-fret');
        },
        generateNoteNames(noteIndex, accidentals, octave) {
            noteIndex = noteIndex % 12;
            let noteName;
            if (accidentals === 'flats') {
                noteName = notesFlat[noteIndex];
            } else if (accidentals === 'sharps') {
                noteName = notesSharp[noteIndex];
            }
            noteName = noteName + octave.toString();

            return noteName;
        }
    }

    const handlers = {
        setAccidentals(event) {
            if (event.target.classList.contains('acc-select')) {
                accidentals = event.target.value;
                app.setupFretboard();
            } else {
                return;
            }
        },
        setupEventListeners() {
            accidentalSelector.addEventListener('click', this.setAccidentals);
        }
    }


    const tools = {
        createElement(element, content) {
            element = document.createElement(element);
            if (arguments.length > 1) {
                element.innerHTML = content;
            }
            return element;
        }
    }


    app.init();
})();


