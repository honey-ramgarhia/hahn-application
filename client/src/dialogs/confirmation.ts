import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';

@inject(DialogController)
export class Confirmation {
    controller: DialogController;

    constructor(controller: DialogController) {
        this.controller = controller;
    }
}


