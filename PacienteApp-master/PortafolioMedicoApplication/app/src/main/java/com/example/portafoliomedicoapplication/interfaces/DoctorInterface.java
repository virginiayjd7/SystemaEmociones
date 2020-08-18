package com.example.portafoliomedicoapplication.interfaces;

public interface DoctorInterface {

    interface ViewMain{
        void handleGetListDoctor();//1
        void handleSuccessGetListDoctor();//5.A
    }

    interface Model{
        void getListDoctor();//3
    }

    interface Presenter{
        void doGetListDoctor();//2
    }

    interface Listener{
        void onSuccessGetListDoctor();//4.A
        void onFailedGetListDoctor();//4.B
    }

    interface RowListener {
        void onClickDoctorRow();//clic en la fila

    }
}
