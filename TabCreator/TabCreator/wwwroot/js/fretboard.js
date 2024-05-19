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
    const numberOfStrings = 6;
    const numberOfFrets = 25;
    const chordNumberOfFrets = 5;
    const noteColors = {
        'default': '#2f4d85',
        'selected': 'black',
        'defaultmouse': '#4370c4',
        'selectedmouse': '#082254'
    }

    let allNotes;
    let accidentals = 'sharps';
    let activeNotes = ['E4', 'B3', 'G3', 'D3', 'A2', 'E2'];

    let chordCompositionString = "s1f00s2f00s3f00s4f00s5f00s6f00"; //all open notes selected;
    /* 
    a note in chordCompositionString has the format: s<string number>f<fret number>
    example: note 23 on string 3 is shown as: s3f23
    this is to avoid accidentally replacing other characters
    */

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
                    noteBtn.id = "note-" + i + "-" + fret;

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

            let startFret = tools.createElement('p');
            startFret.classList.add('start-fret-chord');
            startFret.innerHTML = '1';
            chordSection.appendChild(startFret);

            for (let i = 0; i < numberOfStrings; i++) {

                let string = tools.createElement('div');
                string.classList.add('string-chord');
                chordSection.appendChild(string);

                for (let fret = 0; fret <= chordNumberOfFrets; fret++) {
                    let noteFret = tools.createElement('div');
                    noteFret.classList.add('note-fret-chord');
                    string.appendChild(noteFret);

                    let noteChord = tools.createElement('div');
                    noteChord.classList.add('note-chord');
                    noteChord.classList.add('string-chord-' + i);
                    noteChord.classList.add('fret-chord-' + fret);

                    if (fret == 0) {
                        noteChord.style.opacity = 1;
                    }

                    noteFret.appendChild(noteChord);
                }
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
            let selectedNotes = document.querySelectorAll('.note-selected');
            let fretNumbers = [0, 0, 0, 0, 0, 0];
            let minFret = 30;
            let maxFret = 0;
            let iterator = 1;

            let allNoteChord = document.querySelectorAll('.note-chord');
            for (let note of allNoteChord) {
                note.style.opacity = 0;
                note.classList.remove('fret-muted');
                note.innerHTML = '';
            }

            //Update 'activeNotes' array with fretboard values
            //Find left-most and right-most fret of chord

            for (let element of selectedNotes) {
                let selectedString = element.classList.value.split(" ")[1].split("-")[1];
                selectedString = parseInt(selectedString);
                let selectedFret = element.classList.value.split(" ")[2].split("-")[1];
                selectedFret = parseInt(selectedFret);

                while (iterator < selectedString) {
                    fretNumbers[iterator - 1] = -1;
                    iterator += 1;
                }
                fretNumbers[iterator - 1] = selectedFret;
                iterator += 1;

                if (selectedFret != 0) {
                    if (selectedFret < minFret) {
                        minFret = selectedFret;
                    }
                    if (selectedFret > maxFret) {
                        maxFret = selectedFret;
                    }
                }
            }

            while (iterator < 7) {
                fretNumbers[iterator - 1] = -1;
                iterator++;
            }

            if (minFret == 30 || minFret == 0) {
                minFret = 1;
            }

            if (maxFret - minFret >= 5) {
                document.querySelector('.start-fret-chord').innerHTML = 'Frets too far apart';
            }
            else if (maxFret - minFret < 5) {
                document.querySelector('.start-fret-chord').innerHTML = minFret;

                for (let string = 0; string < numberOfStrings; string++) {
                    let fret;
                    if (fretNumbers[string] > 0) {
                        fret = ((fretNumbers[string] - minFret + 1) % 6);
                    }
                    else {
                        fret = 0;
                    }

                    let stringClass = '.string-chord-' + string;
                    let fretClass = '.fret-chord-' + fret;
                    if (fretNumbers[string] == -1) {
                        fretClass = '.fret-chord-0';
                    }

                    let classToBeSelected = stringClass + fretClass;

                    let currentNote = document.querySelector(classToBeSelected);

                    currentNote.style.opacity = 1;
                    if (fretNumbers[string] == -1) {
                        currentNote.classList.add('fret-muted');
                        currentNote.innerHTML = 'X';
                    }
                }
            }
        },
        setupInitialNotes() {
            var fretboardString = "";
            var fretboardFret = "";

            var chordIterator = 0;

            while (chordIterator < chordCompositionString.length - 1) {
                fretboardString = parseInt((chordCompositionString[chordIterator + 1])) - 1;
                fretboardFret = chordCompositionString.substring(chordIterator + 3, chordIterator + 5);

                if (fretboardFret[0] == '0') {
                    fretboardFret = fretboardFret[1];
                }

                console.log("(Setup Notes) chordCompositionString: " + chordCompositionString);
                console.log("(Setup Notes) fretboardString + fretboardFret: " + fretboardString + fretboardFret);

                let noteFret = document.querySelector("#note-" + fretboardString + "-" + fretboardFret);
                noteFret.classList.add('note-selected');
                noteFret.style.background = noteColors['selected'];

                console.log("(Setup Notes) Note Id: " + noteFret.id);

                fretboardString = parseInt(fretboardString) + 1;

                for (const element of allNotes) {
                    if (element.classList.contains("string-" + fretboardString) && !element.classList.contains("fret-" + fretboardFret)) {

                        console.log("(Setup Notes) OTHER Note Id: " + element.id);

                        element.classList.remove('note-selected');
                        element.style.background = '#2f4d85';
                    }
                }
                
                chordIterator += 5;
            }

            this.updateChord();
        }
    }

    const handlers = {
        setupEventListeners() {
            document.addEventListener("DOMContentLoaded", function () {

                var chordContentString = document.querySelector('#chord-content').value;
                console.log("(On page load) Chord Content String: " + chordContentString);

                if (chordContentString.length > 2) {
                    chordCompositionString = chordContentString;
                }

                console.log("(On page load) Chord Composition String: " + chordCompositionString);

                app.setupInitialNotes();
            });
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

            let currentStringId = 's' + currentString.split("-")[1];
            let currentFretId = currentFret.split("-")[1];

            if (currentFretId < 10) {
                currentFretId = '0' + currentFretId;
            }

            currentFretId = 'f' + currentFretId;

            chordCompositionString = chordCompositionString.replace(currentStringId + currentFretId, "");
            chordCompositionString += currentStringId + currentFretId;

            console.log("(Set Note) Current Id " + currentStringId + currentFretId);

            if (currentElement.classList.contains('note-selected')) {
                currentElement.classList.remove('note-selected');
                currentElement.style.background = '#2f4d85';

                chordCompositionString = chordCompositionString.replace(currentStringId + currentFretId, "");

                activeNotes[stringNumber] = 'X';
            }
            else {
                currentElement.classList.add('note-selected');
                currentElement.style.background = 'black';

                //Deselect all other selected notes on same string

                for (const element of allNotes) {
                    if (element.classList.contains(currentString) && !element.classList.contains(currentFret)) {

                        currentFretId = element.id.split("-")[2];

                        if (currentFretId < 10) {
                            currentFretId = '0' + currentFretId;
                        }

                        currentFretId = 'f' + currentFretId;

                        chordCompositionString = chordCompositionString.replace(currentStringId + currentFretId, "");

                        element.classList.remove('note-selected');
                        element.style.background = '#2f4d85';
                    }
                }
                activeNotes[stringNumber] = currentElement.innerHTML;
            }

            console.log("(Set Note) Chord composition " + chordCompositionString);

            document.querySelector('#user-chord-input').value = chordCompositionString;

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


