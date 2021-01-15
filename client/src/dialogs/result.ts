import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';

@inject(DialogController)
export class Result {
    controller: DialogController;
    errors: string[];

    constructor(controller: DialogController) {
        this.controller = controller;
    }

    activate(error) {
        this.errors = error.errors;
    }
}


