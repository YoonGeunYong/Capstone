using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CreatePNG : MonoBehaviour
{
    public int pixelScale = 1; // 픽셀 스케일 조절
    public string screenshotName = "PixelPerfectScreenshot.png";

    private PixelPerfectCamera pixelPerfectCamera;

    void Start()
    {
        // Pixel Perfect Camera 가져오기
        pixelPerfectCamera = Camera.main.GetComponent<PixelPerfectCamera>();

        if (pixelPerfectCamera == null)
        {
            Debug.LogError("PixelPerfectCamera 컴포넌트를 찾을 수 없습니다. 메인 카메라에 PixelPerfectCamera가 추가되어 있는지 확인하세요.");
        }
    }

    void Update()
    {
        // 사용자 입력 등을 감지하거나 특정 조건에서 스크린샷 찍기
        if (Input.GetKeyDown(KeyCode.P))
        {
            CaptureScreenshot();
        }
    }

    void CaptureScreenshot()
    {
        if (pixelPerfectCamera == null)
        {
            Debug.LogError("PixelPerfectCamera 컴포넌트가 없습니다.");
            return;
        }

        // 현재 Pixel Perfect Camera의 설정을 저장
        int originalAssetsPPU = pixelPerfectCamera.assetsPPU;
        bool originalStretchFill = pixelPerfectCamera.pixelSnapping ? pixelPerfectCamera.cropFrameX : pixelPerfectCamera.cropFrameY;

        // Pixel Perfect 기능을 해제하여 스크린샷을 찍음
        pixelPerfectCamera.assetsPPU *= pixelScale;
        pixelPerfectCamera.pixelSnapping = false;
        pixelPerfectCamera.cropFrameX = false;
        pixelPerfectCamera.cropFrameY = false;

        // 스크린샷 찍기
        ScreenCapture.CaptureScreenshot(screenshotName);

        // Pixel Perfect Camera의 설정을 원래대로 복원
        pixelPerfectCamera.assetsPPU = originalAssetsPPU;
        pixelPerfectCamera.pixelSnapping = true;
        pixelPerfectCamera.cropFrameX = originalStretchFill;
        pixelPerfectCamera.cropFrameY = originalStretchFill;
    }
}

