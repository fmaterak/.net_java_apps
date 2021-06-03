package com.zetcode;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import javax.swing.ImageIcon;
import javax.swing.JPanel;
import javax.swing.Timer;

public class Board extends JPanel implements ActionListener {

    private final int BOARD_WIDTH = 300;
    private final int BOARD_HEIGHT = 300;
    private final int DOT_SIZE = 10;
    private final int ALL_DOTS = 900;
    private final int RAND_POS = 29;
    private final int DELAY = 140;

    private final int[] snake_x = new int[ALL_DOTS];
    private final int[] snake_y = new int[ALL_DOTS];

    private int dots;
    private int apple_x;
    private int apple_y;
    private int move_counter=0;

    private boolean leftDirection = false;
    private boolean rightDirection = true;
    private boolean upDirection = false;
    private boolean downDirection = false;
    private boolean inGame = true;

    private Timer timer;
    private Image snake_body;
    private Image apple;
    private Image head;

    Apple game_apple = new Apple();
    final Thread thread1 = new Thread(game_apple);
    Runnable thread2 = new TAdapter();

    public Board() {
        initBoard();
        thread1.setDaemon(true);
        thread1.start();
        thread2.run();
    }
    
    private void initBoard() {
        addKeyListener(new TAdapter());
        setBackground(Color.black);
        setFocusable(true);

        setPreferredSize(new Dimension(BOARD_WIDTH, BOARD_HEIGHT));
        loadImages();
        initGame();
    }

    private void loadImages() {

        ImageIcon iid = new ImageIcon("src/resources/dot.png");
        snake_body = iid.getImage();

        ImageIcon iia = new ImageIcon("src/resources/apple.png");
        apple = iia.getImage();

        ImageIcon iih = new ImageIcon("src/resources/head.png");
        head = iih.getImage();
    }

    private void initGame() {

        dots = 4;

        for (int z = 0; z < dots; z++) {
            snake_x[z] = 50 - z * 10;
            snake_y[z] = 50;
        }
        
        game_apple.run();

        timer = new Timer(DELAY, this);
        timer.start();
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);

        doDrawing(g);
    }
    private void doDrawing(Graphics g) {

        if (inGame) {
            g.drawImage(apple, apple_x, apple_y, this);
            for (int z = 0; z < dots; z++) {
                if (z == 0) {
                    g.drawImage(head, snake_x[z], snake_y[z], this);
                } else {
                    g.drawImage(snake_body, snake_x[z], snake_y[z], this);
                }
            }
            move_counter++;
            if (move_counter%40 == 0) game_apple.run();
            Toolkit.getDefaultToolkit().sync();

        } else {
            gameOver(g);
        }
    }

    private void gameOver(Graphics g) {
        
        String msg = "Game Over";
        Font small = new Font("Helvetica", Font.BOLD, 20);
        FontMetrics metr = getFontMetrics(small);

        g.setColor(Color.white);
        g.setFont(small);
        g.drawString(msg, (BOARD_WIDTH - metr.stringWidth(msg)) / 2, BOARD_HEIGHT / 2);
    }

    private void checkApple() {

        if ((snake_x[0] == apple_x) && (snake_y[0] == apple_y)) {

            dots++;
            game_apple.run();
        }
    }

    private void move() {

        for (int z = dots; z > 0; z--) {
            snake_x[z] = snake_x[(z - 1)];
            snake_y[z] = snake_y[(z - 1)];
        }

        if (leftDirection) {
            snake_x[0] -= DOT_SIZE;
        }

        if (rightDirection) {
            snake_x[0] += DOT_SIZE;
        }

        if (upDirection) {
            snake_y[0] -= DOT_SIZE;
        }

        if (downDirection) {
            snake_y[0] += DOT_SIZE;
        }
    }

    private void checkCollision() {

        for (int z = dots; z > 0; z--) {

            if ((z > 4) && (snake_x[0] == snake_x[z]) && (snake_y[0] == snake_y[z])) {
                inGame = false;
            }
        }

        if (snake_y[0] >= BOARD_HEIGHT) {
            inGame = false;
        }

        if (snake_y[0] < 0) {
            inGame = false;
        }

        if (snake_x[0] >= BOARD_WIDTH) {
            inGame = false;
        }

        if (snake_x[0] < 0) {
            inGame = false;
        }
        
        if (!inGame) {
            timer.stop();
        }
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        
        if (inGame) {
            checkApple();
            checkCollision();
            move();
        }

        repaint();
    }

    private class Apple extends Thread {
        @Override
        public void run() {
                int rand = (int) (Math.random() * RAND_POS);
                apple_x = ((rand * DOT_SIZE));

                rand = (int) (Math.random() * RAND_POS);
                apple_y = ((rand * DOT_SIZE));

        }
    }

    private class TAdapter extends KeyAdapter implements Runnable {

        @Override
        public void keyPressed(KeyEvent e) {

            int key = e.getKeyCode();

            if ((key == KeyEvent.VK_LEFT) && (!rightDirection)) {
                leftDirection = true;
                upDirection = false;
                downDirection = false;
            }

            if ((key == KeyEvent.VK_RIGHT) && (!leftDirection)) {
                rightDirection = true;
                upDirection = false;
                downDirection = false;
            }

            if ((key == KeyEvent.VK_UP) && (!downDirection)) {
                upDirection = true;
                rightDirection = false;
                leftDirection = false;
            }

            if ((key == KeyEvent.VK_DOWN) && (!upDirection)) {
                downDirection = true;
                rightDirection = false;
                leftDirection = false;
            }
        }

        @Override
        public void run() {

        }
    }
}
