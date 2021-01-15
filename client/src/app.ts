import { PLATFORM } from 'aurelia-framework';

export class App {
    router: any;

    configureRouter(config, router) {
        config.title = 'Aurelia';
        config.options.pushState = true;
        config.map([
            { route: ['', 'home'], name: 'home', nav: true, moduleId: PLATFORM.moduleName('pages/home/home') },
            { route: 'success', name: 'success', nav: true, moduleId: PLATFORM.moduleName('pages/success/success') },
        ]);
  
        this.router = router;
    }
}