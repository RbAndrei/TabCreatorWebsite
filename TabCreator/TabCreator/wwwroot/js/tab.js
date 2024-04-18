(function () {
    const root = document.documentElement;
    const scoreElement = document.querySelector('.score-element');
    const numberOfStrings = 6;
    const numberOfNotes = 18;
    const noteColors = {
        'default': '#2f4d85',
        'defaultmouse': '#4370c4',
    }

    let allNotes;

    const app = {
        init() {
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

                        noteContainer.addEventListener('mouseover', function (event) { handlers.setNoteMouseOver(event); })
                        noteContainer.addEventListener('mouseout', function (event) { handlers.setNoteMouseOut(event); })
                        noteContainer.addEventListener('click', function (event) { handlers.setNoteSelected(event); })

                        noteContainer.appendChild(noteText);
                    }
                }
            }
        }
    }

    const handlers = {
        setupEventListeners() {

        },
        setNoteSelected(event) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');

            let fretNumber = document.querySelector("#fret-number").value;

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

            parentElement.style.color = noteColors['defaultmouse'];
            parentElement.style.background = '#e0e9f7';
            childElement.style.background = '#e0e9f7';
        },
        setNoteMouseOut(event) {
            let [parentElement, childElement] = tools.getParentChild(event.target, 'note-container', 'note-text');

            parentElement.style.color = noteColors['default'];
            parentElement.style.background = 'rgba(0, 0, 0, 0)';
            childElement.style.background = 'white';
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
        getParentElement(element, classNameParent){
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
        }
    }


    app.init();
})();


