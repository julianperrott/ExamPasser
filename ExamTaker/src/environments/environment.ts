import { environmentBase } from './environment.base';

const env: Partial<typeof environmentBase> = {};

export const environment = Object.assign(environmentBase, env);
