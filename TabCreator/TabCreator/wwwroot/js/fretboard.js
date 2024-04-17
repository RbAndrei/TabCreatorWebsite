(function () {
    const root = document.documentElement;
    const fretboard = document.querySelector('.fretboard');
    const chordSection = document.querySelector('.chord');
    const singleFretMarkPositions = [3, 5, 7, 9, 15, 17, 19, 21];
    const doubleFretMarkPositions = [12, 24];
    const notesFlat = ["C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B"];
    const notesSharp = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];
    const openNotesOctave = [4, 3, 3, 3, 2, 2];
    const guitarStandardTuning = [4, 11, 7, 2, 9, 4];
    const noteColors = {
        'default': '#2f4d85',
        'selected': 'black',
        'defaultmouse': '#4370c4',
        'selectedmouse': '#082254'
    }

    let allNotes;
    let accidentals = 'sharps';
    let activeNotes = ['E4', 'B3', 'G3', 'D3', 'A2', 'E2'];
    let numberOfFrets = 25;
    let numberOfStrings = 6;
    let currentOctave = 2;

    const app = {
        init() {
            this.setupFretboard();
            this.setupChordSection();
            handlers.setupEventListeners();
        },
        setupFretboard() {
            fretboard.innerHTML = '';
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

                    let noteBtn = tools.createElement('button');
                    noteBtn.classList.add('note-btn');
                    noteBtn.classList.add('string-' + (i + 1));
                    noteBtn.classList.add('fret-' + fret);

                    if (noteBtn.classList.contains('fret-0')) {
                        noteBtn.style.background = noteColors['selected'];
                        noteBtn.classList.add('note-selected');
                    }

                    noteBtn.addEventListener('click', function (event) { handlers.setNoteSelected(event); });
                    noteBtn.addEventListener('mouseover', function (event) { handlers.setNoteMouseOver(event); })
                    noteBtn.addEventListener('mouseout', function (event) { handlers.setNoteMouseOut(event); })

                    noteFret.appendChild(noteBtn);
                    string.appendChild(noteFret);

                    let noteName = this.generateNoteNames((fret + guitarStandardTuning[i]), accidentals, currentOctave);

                    if (noteName.includes("B") && !noteName.includes("b")) {
                        currentOctave += 1;
                    }

                    noteBtn.innerHTML = noteName;

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
            allNotes = document.querySelectorAll('.note-btn');
        },
        setupChordSection() {
            chordSection.innerHTML = '';
            let noteNames = activeNotes

            for (let i = 0; i < numberOfStrings; i++) {
                let noteNameElement = tools.createElement('p', noteNames[i]);
                noteNameElement.classList.add("string-" + (i + 1));
                noteNameElement.classList.add("pressed-note");
                chordSection.appendChild(noteNameElement);
            }
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
        },
        updateChord() {
            const pressedNotes = document.querySelectorAll('.pressed-note');

            for (let i = 0; i < numberOfStrings; i++) {
                pressedNotes[i].innerHTML = activeNotes[i];
            }
        }
    }

    const handlers = {
        setupEventListeners() {

        },
        setNoteMouseOver(event) {
            let currentElement = event.target;
            if (currentElement.classList.contains('note-selected')) {
                currentElement.style.background = noteColors['selectedmouse'];
            }
            else {
                currentElement.style.background = noteColors['defaultmouse'];
            }
        },
        setNoteMouseOut(event) {
            let currentElement = event.target;
            if (currentElement.classList.contains('note-selected')) {
                currentElement.style.background = noteColors['selected'];
            }
            else {
                currentElement.style.background = noteColors['default'];
            }
        },
        setNoteSelected(event) {
            let currentElement = event.target;
            let currentString = currentElement.classList.value.split(" ")[1];
            let currentFret = currentElement.classList.value.split(" ")[2];
            let stringNumber = currentString.slice(-1) - 1;

            if (currentElement.classList.contains('note-selected')) {
                currentElement.classList.remove('note-selected');
                currentElement.style.background = '#2f4d85';

                activeNotes[stringNumber] = 'X';
            }
            else {
                currentElement.classList.add('note-selected');
                currentElement.style.background = 'black';

                //Deselect all other selected notes on same string

                /*let currentString = currentElement.classList.value.split(" ")[1];
                let currentFret = currentElement.classList.value.split(" ")[2];*/

                for (const element of allNotes) {
                    if (element.classList.contains(currentString) && !element.classList.contains(currentFret)) {
                        element.classList.remove('note-selected');
                        element.style.background = '#2f4d85';
                    }
                }

                activeNotes[stringNumber] = currentElement.innerHTML;
            }
            app.updateChord();
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


