# ReID Generator For Drone #

### 개발 환경 구성 ###
1. `GTAMODSCRIPT` 속에 있는 파일 6개 `dinput8.dll`, `NativeTrainer.asi`, `ScriptHookV.dll`, `ScriptHookDotNet.asi`, `ScriptHookDotNet2.dll`, `ScriptHookDotNet3.dll` 를 모두 GTA5가 설치된 경로에 붙여넣는다.
![image](https://user-images.githubusercontent.com/72537190/191684922-ee4e3322-48a7-4f7d-a080-f4a5a010e26e.png)

2. GTA5가 설치된 경로에 scripts 폴더를 생성한다.
![image](https://user-images.githubusercontent.com/72537190/191684971-737195df-0686-443b-b161-04654a3fadc9.png)

3. Visual Studio 2022로 해당 프로젝트를 연다.

4. 주석으로 `[SET]`이라고 되어 있는 부분을 코드 내에서 검색하고, 해당 주석 밑 코드를 자신의 컴퓨터에 맞게 조절한다. (총 다섯군데 세팅 필요)
	```
	//[SET] 카메라 좌표 설정(고도 조절은 Z 좌표 수정)
	//[SET] 화면 사이즈 설정 - 1
	//[SET] 화면 사이즈 설정 - 2
	//[SET] 이미지 저장 경로 설정 - 1
	//[SET] 이미지 저장 경로 설정 - 2
	```

5. 빌드 이벤트를 자신의 컴퓨터의 GTA5 설치 경로 내 scripts 폴더로 설정한다. 
	```
	<예시>
	COPY "$(TargetPath)" "D:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V\scripts"
	```
	![image](https://user-images.githubusercontent.com/72537190/191683538-275f0971-87ef-4ccf-9ad3-7cfa73445faf.png)
6. 빌드하고 scripts 폴더 내에 dll이 생성되었는지 확인한다.
![image](https://user-images.githubusercontent.com/72537190/191685357-83f61729-7c06-4a58-afb0-78dd8e379c06.png)



### 사용법 ###

1. **오프라인**으로 GTA5 실행

2. `F6` 을 눌러서 툴 메뉴 실행 (`F7` 누르면 닫힘)
3. Numpad에 있는  `8` `2` `5` 로 기능 선택 및 실행
	* **CaptureForDrone**: 드론용 사진 촬영
	* **Weather**: 날씨 변경
	* **Hour Forward**, **Hour Back**: 시간조절
4. CaptureForDrone 기능으로 데이터셋 생성 시 scripts 폴더 내에 로그가 생성됨
