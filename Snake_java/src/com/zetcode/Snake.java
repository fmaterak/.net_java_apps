package com.zetcode;

import java.awt.EventQueue;
import javax.swing.*;

public class Snake extends JFrame {
    public Snake() {
        initUI();
    }
    
    private void initUI() {
        add(new Board());
               
        setResizable(false);
        pack();
        
        setTitle("Snake");
        setLocationRelativeTo(null);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }


    public static void main(String[] args) {
        EventQueue.invokeLater(() -> {
            JFrame game_window = new Snake();
            game_window.setVisible(true);
        });
    }
}
class DisplayMessage implements Runnable {
    Board a = new Board();

    public void run() {
        while(true) {
            System.out.println('m');
        }
    }
}