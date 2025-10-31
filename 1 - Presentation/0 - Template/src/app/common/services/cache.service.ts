import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { ECacheType } from '../enum/type-cache.enum';
//import { ECacheType } from 'app/common/type-cache.enum';



@Injectable()
export class CacheService {

    public static get(key: string, type: ECacheType) {
        if (type === ECacheType.LOCAL) {
            return LocalStorageService.get(key);
        }
        return null;
    }

    public static add(key: string, data: any, type: ECacheType) {
        if (type === ECacheType.LOCAL) {
            LocalStorageService.add(key, data);
        }
    }

    public static update(key: string, data: any, type: ECacheType) {
        if (type === ECacheType.LOCAL) {
            LocalStorageService.add(key, data);
        }
    }

    public static remove(key: string, type: ECacheType) {
        if (type === ECacheType.LOCAL) {
            LocalStorageService.remove(key);
        }
    }

    public static removePartialKey(key: string, type: ECacheType) {
        if (type === ECacheType.LOCAL) {
            LocalStorageService.remove(key);
        }
    }

    public static reset(type?: ECacheType) {
        if (type === ECacheType.LOCAL) {
            LocalStorageService.reset();
        } else {
            LocalStorageService.reset();
        }
    }
}
