/* ********************************************************
    Things to remember:

    Each tab has an id of form: tab-x , where x is the number of the tabBlock
    Each string has an id of form: tab-x-y , where x is the number of the tabBlock the string is a child of
    and y is the number of the string in that tab block
    Each note container has an id of form: note-x-y-z , where x is the number of the tabBlock the string is a child of,
    y is the number of the string that the note is a part of, z is the number of the note on the string.

    The tab composition string will contain the ids of the notes that are currently used and:
        - if the noteContainer contains a fret number, the number of the fret. NOTE: the fret number will always
                                                                               be made up of 2 numbers (e.g 00, 07, 12, 35)
        - if the noteContainer contains an articultation: - sl => slide
                                                          - po => pull-off
                                                          - ho => hammer-on
******************************************************** */

(function () {
    const root = document.documentElement;
    const scoreElement = document.querySelector('.score-element');
    const numberOfStrings = 6;
    const numberOfNotes = 36;

    let tabCompositionString = '';
    let selectedElement = document.querySelector('#fret-number');

    const app = {
        init() {
            selectedElement.focus();
            selectedElement.parentElement.classList.add('element-selected');
            this.setupTabBlock();
            handlers.setupEventListeners();
        },
        setupTabBlock() {

            for (let blockNumber = 0; blockNumber < 15; blockNumber++) {
                let tabBlock = tools.createElement('div');
                tabBlock.id = "tab-" + blockNumber;
                tabBlock.classList.add('tab-block');

                scoreElement.appendChild(tabBlock);

                tabBlock.innerHTML = '';
                // Add strings to tabBlock
                for (let stringNumber = 0; stringNumber < numberOfStrings; stringNumber++) {
                    let string = tools.createElement('div');
                    string.id = "string-" + blockNumber + "-" + stringNumber;
                    string.classList.add('string');
                    tabBlock.appendChild(string);

                    // Create note containers
                    for (let noteNumber = 0; noteNumber <= numberOfNotes; noteNumber++) {
                        let noteContainer = tools.createElement('div');
                        noteContainer.id = "note-" + blockNumber + "-" + stringNumber + "-" + noteNumber;
                        noteContainer.classList.add('note-container');

                        string.appendChild(noteContainer);

                        let noteText = tools.createElement('p');
                        noteText.classList.add('note-text');

                        noteContainer.appendChild(noteText);

                        noteContainer.addEventListener('mouseover', function (event) { handlers.setNoteMouseOver(event); })
                        noteContainer.addEventListener('mouseout', function (event) { handlers.setNoteMouseOut(event); })
                        noteContainer.addEventListener('click', function (event) { handlers.setNoteSelected(event); })

                        
                    }
                }
            }
        },
        setupInitialNotes() {
            var tabBlock = "";
            var tabString = "";
            var tabNoteContainer = "";
            var tabNoteValue = "";

            var blockIterator = 0;

            while (blockIterator < tabCompositionString.length - 1) {
                tabBlock = tabCompositionString.substring(blockIterator, blockIterator + 2);
                tabString = tabCompositionString[blockIterator + 2];
                tabNoteContainer = tabCompositionString.substring(blockIterator + 3, blockIterator + 5);
                tabNoteValue = tabCompositionString.substring(blockIterator + 5, blockIterator + 7);

                if (tabBlock[0] == '0') {
                    tabBlock = tabBlock[1];
                }
                if (tabNoteContainer[0] == '0') {
                    tabNoteContainer = tabNoteContainer[1];
                }
                if (tabNoteValue[0] == '0') {
                    tabNoteValue = tabNoteValue[1];
                }

                switch (tabNoteValue) {
                    case "sl":
                        tabNoteValue = "/";
                        break;
                    case "po":
                        tabNoteValue = "p";
                        break;
                    case "ho":
                        tabNoteValue = "h";
                        break;
                }

                console.log("(Setup notes) Block Id: " + tabBlock);
                console.log("(Setup notes) String Id: " + tabString);
                console.log("(Setup notes) Container Id: " + tabNoteContainer);
                console.log("(Setup notes) Note value: " + tabNoteValue);

                var noteContainerId = "note-" + tabBlock + "-" + tabString + "-" + tabNoteContainer;

                var noteContainer = document.querySelector("#" + noteContainerId);

                console.log("(Setup notes) Container Id: " + noteContainer.id);

                let customEvent = new CustomEvent('noteSelected', {
                    detail: { noteValue: tabNoteValue }
                });

                noteContainer.dispatchEvent(customEvent);

                blockIterator += 7;
            }
        }
    }

    const handlers = {
        setupEventListeners() {
            let elementList = [];

            for (let element of document.querySelectorAll('#sidebar ul li p')) {
                elementList.push(element);
            }

            elementList.push(document.querySelector('#fret-number'));
            elementList.push(document.querySelector('#label-fret-number'));

            for (let element of elementList) {
                element.addEventListener('click', (event) => this.setSelectedElement(event, elementList));
            }      

            document.addEventListener("DOMContentLoaded", function () {

                document.querySelectorAll('.note-container').forEach(container => {
                    container.addEventListener('noteSelected', function (event) {
                        handlers.setNoteSelected(event, event.detail.noteValue);
                    })
                })

                tabCompositionString = document.querySelector('#tablature-content').value;
                console.log("(On page load) Tab Composition String: " + tabCompositionString);

                if (tabCompositionString != null) {
                    app.setupInitialNotes();
                }
            });
        },
        setSelectedElement(event, otherElements) {
            let currentElement = event.target;

            for (let element of otherElements) {
                if (element.classList.contains('element-selected')) {
                    element.classList.remove('element-selected');
                }
                if (element.parentElement.classList.contains('element-selected')) {
                    element.parentElement.classList.remove('element-selected');
                }
            }

            if (currentElement.id == 'label-fret-number') {
                currentElement.parentElement.classList.add('element-selected');
                selectedElement = currentElement.nextSibling;
                selectedElement.focus();
            }
            else if (currentElement.id == 'fret-number') {
                currentElement.parentElement.classList.add('element-selected');
                selectedElement = currentElement;
            }
            else {
                currentElement.classList.add('element-selected');
                selectedElement = currentElement;
            }
        },
        setNoteSelected(event, noteValue = -1) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');

            let fretNumber = 0;

            let currentId = parentElement.id;
            let currentBlockId = parseInt(currentId.split("-")[1]);
            if (currentBlockId < 10) {
                currentBlockId = '0' + currentBlockId;
            }
            let currentStringId = currentId.split("-")[2];

            let currentNoteId = parseInt(currentId.split("-")[3]);
            if (currentNoteId < 10) {
                currentNoteId = '0' + currentNoteId;
            }

            currentId = currentBlockId + currentStringId + currentNoteId;

            if (selectedElement.value === undefined) {
                fretNumber = selectedElement.innerHTML;
            }
            else {
                fretNumber = selectedElement.value;
                if (fretNumber > 25) {
                    fretNumber = 25;
                }
                else if (fretNumber < 0) {
                    fretNumber = 0;
                }
            }

            if (parentElement.classList.contains('note-selected')) {
                parentElement.classList.remove('note-selected');

                switch (childElement.innerHTML) {
                    case 'p':
                        currentId += 'po';
                        break;
                    case 'h':
                        currentId += 'ho';
                        break;
                    case '/':
                        currentId += 'sl';
                        break;
                    default:
                        if (childElement.innerHTML < 10) {
                            currentId += '0' + childElement.innerHTML;
                        }
                        else currentId += childElement.innerHTML;
                        break;
                }

                childElement.innerHTML = '';
                
                tabCompositionString = tabCompositionString.replace(currentId, '');
            }
            else {
                parentElement.classList.add('note-selected');

                if (noteValue == -1) {
                    childElement.innerHTML = fretNumber;

                    switch (childElement.innerHTML) {
                        case 'p':
                            currentId += 'po';
                            break;
                        case 'h':
                            currentId += 'ho';
                            break;
                        case '/':
                            currentId += 'sl';
                            break;
                        default:
                            if (childElement.innerHTML < 10) {
                                currentId += '0' + childElement.innerHTML;
                            }
                            else currentId += childElement.innerHTML;
                            break;
                    }

                    tabCompositionString += currentId;
                }
                else {
                    childElement.innerHTML = noteValue;
                }    
            }   

            console.log(tabCompositionString);
            document.querySelector('#user-tab-input').value = tabCompositionString;
        },
        setNoteMouseOver(event) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');
            let fretNumber = parseInt(childElement.innerHTML);

            parentElement.classList.add('container-hover');
            childElement.classList.add('container-hover');
        },
        setNoteMouseOut(event) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');

            parentElement.classList.remove('container-hover');
            childElement.classList.remove('container-hover');
        }
    }


    const tools = {
        createElement(element, content) {
            element = document.createElement(element);
            if (arguments.length > 1) {
                element.innerHTML = content;
            }
            return element;
        },
        getParentElement(element, classNameParent) {
            if (element.classList.contains(classNameParent)) {
                return element;
            }
            else {
                return element.parentElement;
            }
        },
        getChildElement(element, classNameChild) {
            if (element.classList.contains(classNameChild)) {
                return element;
            }
            else {
                return element.childNodes[0];
            }
        },
        getParentChild(element, classNameParent, classNameChild) {
            return [this.getParentElement(element, classNameParent), this.getChildElement(element, classNameChild)]
        },
        containsClass(element, className) {
            return element.classList.contains(className);
        }
    }


    app.init();
})();


