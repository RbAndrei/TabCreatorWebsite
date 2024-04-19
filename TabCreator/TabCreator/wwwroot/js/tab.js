(function () {
    const root = document.documentElement;
    const scoreElement = document.querySelector('.score-element');
    const numberOfStrings = 6;
    const numberOfNotes = 36;
    const noteColors = {
        'default': '#2f4d85',
        'defaultmouse': '#4370c4',
    }

    let allNotes;
    let selectedElement = document.querySelector('#fret-number');

    const app = {
        init() {
            selectedElement.focus();
            selectedElement.parentElement.classList.add('element-selected');
            this.setupTabBlock();
            handlers.setupEventListeners();
        },
        setupTabBlock() {

            for (let stuff = 0; stuff < 15; stuff++) {
                let tabBlock = tools.createElement('div');
                tabBlock.classList.add('tab-block');

                scoreElement.appendChild(tabBlock);

                tabBlock.innerHTML = '';
                // Add strings to tabBlock
                for (let i = 0; i < numberOfStrings; i++) {
                    let string = tools.createElement('div');
                    string.classList.add('string');
                    tabBlock.appendChild(string);

                    // Create note containers
                    for (let noteNumber = 0; noteNumber <= numberOfNotes; noteNumber++) {
                        let noteContainer = tools.createElement('div');
                        noteContainer.classList.add('note-container');

                        string.appendChild(noteContainer);

                        let noteText = tools.createElement('p');
                        noteText.classList.add('note-text');

                        noteContainer.appendChild(noteText);

                        if (noteNumber % 2 == 1) {
                            noteContainer.classList.add('articulation');
                        }

                        noteContainer.addEventListener('mouseover', function (event) { handlers.setNoteMouseOver(event); })
                        noteContainer.addEventListener('mouseout', function (event) { handlers.setNoteMouseOut(event); })
                        noteContainer.addEventListener('click', function (event) { handlers.setNoteSelected(event); })

                        
                    }
                }
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
        setNoteSelected(event) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');

            let fretNumber = 0;

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
                childElement.innerHTML = '';
            }
            else {
                parentElement.classList.add('note-selected');
                childElement.innerHTML = fretNumber;
            }   
        },
        setNoteMouseOver(event) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');
            let fretNumber = parseInt(childElement.innerHTML);

            parentElement.classList.add('container-hover');
            childElement.classList.add('container-hover');
        },
        setNoteMouseOut(event) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');

            parentElement.style.color = noteColors['default'];
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


